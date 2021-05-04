// <copyright file="IgnoreAuthenticationAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Ignore Authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreAuthenticationAttribute : Attribute, IFilterMetadata
    {
    }
}
