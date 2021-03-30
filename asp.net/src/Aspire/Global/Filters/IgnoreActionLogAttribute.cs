// <copyright file="IgnoreActionLogAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// 响应 日志 忽略.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class IgnoreActionLogAttribute : Attribute, IFilterMetadata
    {
    }
}
