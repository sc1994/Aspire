using System;

namespace Aspire
{
    /// <summary>
    ///     认证.
    /// </summary>
    public class AuthAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthAttribute" /> class.
        ///     在 controller 或 action 上设置认证的角色.
        /// </summary>
        /// <param name="roles">角色.</param>
        public AuthAttribute(params string[] roles)
        {
            Roles = roles;
        }

        /// <summary>
        ///     Gets 角色.
        /// </summary>
        public string[] Roles { get; }
    }
}