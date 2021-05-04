// <copyright file="IApplication.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using Panda.DynamicWebApi;
    using Panda.DynamicWebApi.Attributes;

    /// <summary>
    /// 应用
    /// 作为控制器的最基层.
    /// </summary>
    [DynamicWebApi]
    [ResponseFormat]
    public interface IApplication : IDynamicWebApi
    {
    }
}
