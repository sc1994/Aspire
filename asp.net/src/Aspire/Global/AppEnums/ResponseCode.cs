// <copyright file="ResponseCode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System.ComponentModel;

    /// <summary>
    /// 响应编码.
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 成功.
        /// </summary>
        [Description("成功")]
        Ok = 20000,

        /// <summary>
        /// 未授权.
        /// </summary>
        [Description("未授权")]
        Unauthorized = 40100,

        /// <summary>
        /// 未授权的角色.
        /// </summary>
        [Description("未授权的角色")]
        UnauthorizedRoles = 40101,

        /// <summary>
        /// 未授权的账号或者密码.
        /// </summary>
        [Description("未授权的账号或者密码")]
        UnauthorizedAccountOrPassword = 40102,

        /// <summary>
        /// 鉴权过期.
        /// </summary>
        [Description("授权过期")]
        AuthorizeExpired = 40103,

        /// <summary>
        /// 授权无效.
        /// </summary>
        [Description("授权无效")]
        AuthorizeInvalid = 40104,

        /// <summary>
        /// 内部服务异常.
        /// </summary>
        [Description("内部服务异常")]
        InternalServerError = 50000,

        /// <summary>
        /// 内部服务数据库异常.
        /// </summary>
        [Description("内部服务数据库异常")]
        InternalServerDatabaseError = 50001,
    }
}