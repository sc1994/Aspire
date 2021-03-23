// <copyright file="SerilogElasticSearchAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.SystemLog
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Aspire.Logger;
    using Aspire.Serilog.ElasticSearch.Provider;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// SerilogElastic Search AppService.
    /// </summary>
    public class SerilogElasticSearchAppService : Application, ISystemLogAppService<
        string,
        SystemLogFilterInputDto,
        SystemLogFilterOutputDto,
        SystemLogDetailOutputDto>
    {
        private readonly LogItemsStore itemsStore;
        private readonly ILogWriter logWriter;
        private readonly string node;
        private readonly string index;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogElasticSearchAppService"/> class.
        /// </summary>
        /// <param name="config">Configuration.</param>
        /// <param name="itemsStore">Items Store.</param>
        /// <param name="logWriter">Log Writer.</param>
        public SerilogElasticSearchAppService(
            IConfiguration config,
            LogItemsStore itemsStore,
            ILogWriter logWriter)
        {
            this.itemsStore = itemsStore;
            this.logWriter = logWriter;
            this.node = config.GetConnectionString("ElasticSearch");
            this.index = config.GetConnectionString("ElasticSearchIndex");
        }

        private enum OperatorEnum
        {
            Term,
            Gte,
            Lte,
        }

        /// <inheritdoc />
        public async Task<PagedResultDto<SystemLogFilterOutputDto>> FilterAsync(SystemLogFilterInputDto filterInput)
        {
            var items = new List<object>();
            if (!string.IsNullOrWhiteSpace(filterInput.ApiMethod))
            {
                items.Add(GetQueryItem("fields.apiMethod.keyword", filterInput.ApiMethod, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.ApiRouter))
            {
                items.Add(GetQueryItem("fields.apiRouter.keyword", filterInput.ApiRouter, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.Title))
            {
                items.Add(GetQueryItem("fields.title.keyword", filterInput.Title, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.Filter1))
            {
                items.Add(GetQueryItem("fields.filter1.keyword", filterInput.Filter1, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.Filter2))
            {
                items.Add(GetQueryItem("fields.filter2.keyword", filterInput.Filter2, OperatorEnum.Term));
            }

            if (filterInput.CreatedAtRange?.Length == 2)
            {
                items.Add(GetQueryItem("@timestamp", filterInput.CreatedAtRange[0], OperatorEnum.Gte));
                items.Add(GetQueryItem("@timestamp", filterInput.CreatedAtRange[1], OperatorEnum.Lte));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.TraceId))
            {
                items.Add(GetQueryItem("fields.traceId.keyword", filterInput.TraceId, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.ClientAddress))
            {
                items.Add(GetQueryItem("fields.clientAddress.keyword", filterInput.ClientAddress, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.ServerAddress))
            {
                items.Add(GetQueryItem("fields.serverAddress.keyword", filterInput.ServerAddress, OperatorEnum.Term));
            }

            if (!string.IsNullOrWhiteSpace(filterInput.Level))
            {
                items.Add(GetQueryItem("level.keyword", filterInput.Level, OperatorEnum.Term));
            }

            var dsl = new
            {
                from = (filterInput.PageIndex - 1) * filterInput.PageSize,
                size = filterInput.PageSize,
                query = new
                {
                    @bool = new
                    {
                        filter = items,
                    },
                },
                sort = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "@timestamp", new { order = "DESC" } },
                    },
                },
            };
            var uri = $"/{this.index}*/_search";
            this.logWriter.Information("Serilog ElasticSearch Dsl", new { uri, dsl });

            using var client = new HttpClient { BaseAddress = new Uri(this.node) };
            var res = await client.PostAsJsonAsync(uri, dsl);
            var data = await res.Content
                .ReadAsStringAsync()
                .DeserializeObjectAsync<JObject>();

            return new PagedResultDto<SystemLogFilterOutputDto>(
                data["hits"]["hits"]
                    .Select(x => ToLogModel<SystemLogFilterOutputDto>(x, 200))
                    .OrderByDescending(x => x.CreatedAt),
                data["hits"]["total"]["value"].ToObject<int>());
        }

        /// <inheritdoc />
        public async Task<SystemLogDetailOutputDto> GetAsync(string id)
        {
            using var client = new HttpClient { BaseAddress = new Uri(this.node) };
            var data = await client.GetStringAsync(id).DeserializeObjectAsync<JObject>();
            return ToLogModel<SystemLogDetailOutputDto>(data, int.MaxValue);
        }

        /// <inheritdoc />
        public async Task<SystemLogSelectItemsDto> GetSelectItems()
        {
            var items = LogItemsStore.GetItems().Select(x => x.DeserializeObject());
            return await Task.FromResult(new SystemLogSelectItemsDto
            {
                ApiMethod = items.Select(x => x["apiMethod"]?.ToString()).Where(x => !x.IsNullOrWhiteSpace()).Distinct().ToArray(),
                ServerAddress = items.Select(x => x["serverAddress"]?.ToString()).Where(x => !x.IsNullOrWhiteSpace()).Distinct().ToArray(),
                ApiRouter = items.Select(x => x["apiRouter"]?.ToString()).Where(x => !x.IsNullOrWhiteSpace()).Distinct().ToArray(),
                Title = items.Select(x => x["title"]?.ToString()).Where(x => !x.IsNullOrWhiteSpace()).Distinct().ToArray(),
            });
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAllSelectItems()
        {
            this.itemsStore.ClearItemsStore();
            return await Task.FromResult(true);
        }

        /// <inheritdoc />
        public async Task<JObject> GetPageConfig()
        {
            // TODO 响应具体的实体
            var config = await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/PageConfig.json"));
            return config.DeserializeObject();
        }

        private static object GetQueryItem(string field, object value, OperatorEnum operatorEnum)
        {
            return new
            {
                @bool = new
                {
                    filter = operatorEnum switch
                    {
                        OperatorEnum.Term => new
                        {
                            term = new Dictionary<string, object>
                            {
                                { field, value },
                            },
                        },
                        OperatorEnum.Gte => new
                        {
                            range = new Dictionary<string, object>
                            {
                                { field, new { gte = value, } },
                            },
                        },
                        OperatorEnum.Lte => (object)new
                        {
                            range = new Dictionary<string, object>
                            {
                                { field, new { lte = value, } },
                            },
                        },
                        _ => throw new ArgumentOutOfRangeException(nameof(operatorEnum), operatorEnum, null),
                    },
                },
            };
        }

        private static TOutput ToLogModel<TOutput>(JToken x, int substringCount)
            where TOutput : SystemLogFilterOutputDto, new()
        {
            return new TOutput
            {
                TraceId = x["_source"]["fields"]["traceId"]?.ToString() ?? string.Empty,
                ApiRouter = $"[{x["_source"]["fields"]["apiMethod"]}] {x["_source"]["fields"]["apiRouter"]}",
                Title = x["_source"]["fields"]["title"]?.ToString() ?? string.Empty,
                Message = x["_source"]["fields"]["message"]?.ToString()?.SubstringSafe(substringCount) ?? string.Empty,
                CreatedAt = x["_source"]["@timestamp"].ToObject<DateTime>(),
                Filter1 = x["_source"]["fields"]["f1"]?.ToString() ?? string.Empty,
                Filter2 = x["_source"]["fields"]["f2"]?.ToString() ?? string.Empty,
                Id = $"/{x["_index"]}/{x["_type"]}/{x["_id"]}",
                Level = x["_source"]["level"].ToString() ?? string.Empty,
                ServerAddress = x["_source"]["fields"]["serverAddress"]?.ToString() ?? string.Empty,
                ClientAddress = x["_source"]["fields"]["clientAddress"]?.ToString() ?? string.Empty,
            };
        }
    }
}