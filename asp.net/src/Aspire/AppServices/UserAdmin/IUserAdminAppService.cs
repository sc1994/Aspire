// <copyright file="IUserAdminAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.UserAdmin
{
    /// <summary>
    /// User Manage AppService.
    /// </summary>
    /// <typeparam name="TUserAdminEntity">UserAdminEntity.</typeparam>
    /// <typeparam name="TUserAdminPrimaryKey">User Manage PrimaryKey.</typeparam>
    /// <typeparam name="TUserAdminPageInputDto">User Manage Page InputDto.</typeparam>
    /// <typeparam name="TUserAdminOutputDto">User Manage OutputDto.</typeparam>
    /// <typeparam name="TUserAdminCreateDto">User Manage CreateDto.</typeparam>
    /// <typeparam name="TUserAdminUpdateDto">User Manage UpdateDto.</typeparam>
    public interface IUserAdminAppService<
        TUserAdminEntity,
        in TUserAdminPrimaryKey,
        in TUserAdminPageInputDto,
        TUserAdminOutputDto,
        in TUserAdminCreateDto,
        in TUserAdminUpdateDto> : ICrudApplication<
        TUserAdminEntity,
        TUserAdminPrimaryKey,
        TUserAdminPageInputDto,
        TUserAdminOutputDto,
        TUserAdminCreateDto,
        TUserAdminUpdateDto>
        where TUserAdminEntity : IAuditEntity<TUserAdminPrimaryKey>
        where TUserAdminPageInputDto : PageInputDto
        where TUserAdminUpdateDto : IDto<TUserAdminPrimaryKey>
    {

    }
}
