using Aspire.Application.AppServices.Dtos;
using Aspire.Domain.Entities;

namespace AutoMapper
{
    public static class AspireProfile
    {
        /// <summary>
        /// 屏蔽将CommonDto中的基础值转到entity中
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreCommonDto<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
            where TSource : CommonDto
            where TDestination : IBaseEntity
        {
            return mapping
                .ForMember(x => x.IsDeleted, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.Ignore())
                .ForMember(x => x.DeletedAt, x => x.Ignore())
                .ForMember(x => x.UpdatedAt, x => x.Ignore());
        }
    }
}
