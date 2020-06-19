﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Aspire.Application.AppServices.Dtos;
using Aspire.Domain.Entities;
using Aspire.Domain.Repositories;
using Aspire.Map;

using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.AppServices
{
    public abstract class CrudAppService<TEntity, TCommonDto> : CrudAppService<TEntity, TCommonDto, long>
        where TEntity : BaseEntity<long>
        where TCommonDto : CommonDto
    {
        public CrudAppService(IRepository<TEntity, long> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TCommonDto, TId> : CrudAppService<TEntity, TCommonDto, TId, TCommonDto>
        where TEntity : BaseEntity<TId>
        where TCommonDto : CommonDto<TId>
    {
        public CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TInputDto> : CrudAppService<TEntity, TOutputDto, TId, TInputDto, TInputDto>
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TInputDto : InputDto<TId>
    {
        public CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }

        [HttpPost("/api/[controller]/create-or-update/{id?}")]
        public virtual async Task<TOutputDto> CreateOrUpdateAsync(TInputDto input, TId id = default(TId))
        {
            if (id?.Equals(default(TId)) ?? true)
                return await base.CreateAsync(input);
            return await base.UpdateAsync(id, input);
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto, TSearchDto> : CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TUpdateDto : UpdateDto<TId>

    {
        public CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto> : AppService
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TUpdateDto : UpdateDto<TId>
    {
        private readonly IRepository<TEntity, TId> _repository;
        private readonly IAspireMapper _mapper;

        public CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("/api/[controller]")]
        public virtual async Task<TOutputDto> CreateAsync([FromBody] TCreateDto input)
        {
            var r = await _repository.AddThenEntityAsync(_mapper.To<TEntity>(input));
            return _mapper.To<TOutputDto>(r);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public virtual async Task<bool> DeleteAsync(TId id)
           => await _repository.DeleteAsync(id);

        [HttpPut("/api/[controller]/{id}")]
        public virtual async Task<TOutputDto> UpdateAsync(TId id, [FromBody] TUpdateDto input)
        {
            var e = await _repository.GetByIdAsync(id);
            _mapper.To(input, e);
            e.Id = id;
            var r = await _repository.UpdateThenEntityAsync(e);
            return _mapper.To<TOutputDto>(r);
        }

        [HttpGet("/api/[controller]/{id}")]
        public virtual async Task<TOutputDto> GetAsync(TId id)
        {
            var r = await _repository.GetByIdAsync(id);
            return _mapper.To<TOutputDto>(r);
        }

        [HttpGet("/api/[controller]/form-params/{form}")]
        public virtual object GetFormParams(string form)
        {
            switch (form)
            {
                case "create":
                    var instance = Activator.CreateInstance(typeof(TCreateDto));
                    return typeof(TCreateDto).GetProperties().Select(x =>
                    {
                        var attribute = x.GetCustomAttributes().FirstOrDefault(x => x.GetType().BaseType == typeof(AspireFormAttribute));
                        if (attribute == null) return null;

                        return ((AspireFormAttribute)attribute).Format(x, instance);
                    }).Where(x => x != null);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public abstract class AspireFormAttribute : Attribute
    {
        public AspireFormAttribute(string title)
        {
            Title = title;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; }

        public abstract object Format(PropertyInfo property, object dtoInstance = null);
    }
}
