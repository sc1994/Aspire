// <copyright file="IUserAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Users
{
    /// <summary>
    /// User Manage AppService.
    /// </summary>
    /// <typeparam name="TUserEntity">User Entity.</typeparam>
    /// <typeparam name="TPrimaryKey">User PrimaryKey.</typeparam>
    /// <typeparam name="TUserPageInputDto">User Page InputDto.</typeparam>
    /// <typeparam name="TUserOutputDto">User OutputDto.</typeparam>
    /// <typeparam name="TUserCreateDto">User CreateDto.</typeparam>
    /// <typeparam name="TUserUpdateDto">User UpdateDto.</typeparam>
    public interface IUserAppService<
        TUserEntity,
        in TPrimaryKey,
        in TUserPageInputDto,
        TUserOutputDto,
        in TUserCreateDto,
        in TUserUpdateDto> : ICrudApplication<
        TUserEntity,
        TPrimaryKey,
        TUserPageInputDto,
        TUserOutputDto,
        TUserCreateDto,
        TUserUpdateDto>
        where TUserEntity : IUser<TPrimaryKey>
        where TUserPageInputDto : IUserPageInputDto
        where TUserCreateDto : IUserCreateDto
        where TUserUpdateDto : IUserUpdateDto<TPrimaryKey>
    {
    }
}
