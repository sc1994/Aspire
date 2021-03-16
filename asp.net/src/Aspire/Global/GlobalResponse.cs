// <copyright file="GlobalResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Global Response.
    /// </summary>
    public class GlobalResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        public GlobalResponse(int code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        public GlobalResponse(ResponseCode code)
            : this((int)code)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="result">Result.</param>
        public GlobalResponse(int code, object result)
            : this(code)
        {
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="result">Result.</param>
        public GlobalResponse(ResponseCode code, object result)
            : this((int)code, result)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="ex">Friendly Exception.</param>
        public GlobalResponse(Exception ex)
            : this(ResponseCode.InternalServerError)
        {
#if DEBUG
            this.StackTrace = ex.StackTrace;
#endif
            this.Title = ex.Message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        /// <param name="ex">Friendly Exception.</param>
        public GlobalResponse(FriendlyException ex)
            : this(ex.Code, ex.Result)
        {
            this.Messages = ex.Messages;
#if DEBUG
            this.StackTrace = ex.StackTrace;
#endif
            this.Title = ex.Title;
        }

        /// <summary>
        /// Gets Code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Gets or sets Messages.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] Messages { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Gets Result.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; }

#if DEBUG
        /// <summary>
        /// Gets or sets Stack Trace.
        /// </summary>
        [JsonIgnore]
        public object StackTrace { get; set; }

        /// <summary>
        /// Gets Stack Trace Text.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] StackTraceText
        {
            get
            {
                if (this.StackTrace is null)
                {
                    return null;
                }

                return this.StackTrace switch
                {
                    FriendlyException friendlyException => friendlyException.StackTrace.ToString()
                        .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(x => !x.Contains(
                            $"{nameof(FriendlyThrowException)}.{nameof(FriendlyThrowException.ThrowException)}"))
                        .Where(x => !x.Contains("System.Runtime"))
                        .Where(x => !x.Contains("Microsoft.AspNetCore"))
                        .Where(x => !x.Contains("Swashbuckle.AspNetCore"))
                        .Where(x => !x.Contains("System.Threading"))
                        .ToArray(),
                    Exception exception => exception.StackTrace
                        ?.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray(),
                    EnhancedStackTrace _ => this.StackTrace.ToString()
                        ?.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray(),
                    StackTrace _ => this.StackTrace.ToString()
                        ?.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray(),
                    _ => new[] { this.StackTrace.ToString() }
                };
            }
        }
#endif

        /// <summary>
        /// Async Write To Http Response.
        /// </summary>
        /// <param name="context">Http Context.</param>
        /// <returns>Task.</returns>
        public async Task WriteToHttpResponseAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(this.SerializeObject());
        }
    }
}
