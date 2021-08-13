using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Aspire.Core.Jwt
{
    /// <summary>
    /// jwt 管理类.
    /// </summary>
    public static class JwtManage
    {
        private static string secretConfig;
        private static int expiredSecondConfig;

        /// <summary>
        /// 初始化 jwt 需要的内容.
        /// </summary>
        /// <param name="secret">密钥.</param>
        /// <param name="expiredSecond">失效时间(秒).</param>
        public static void InitJwtManage(string secret, int expiredSecond)
        {
            JwtManage.secretConfig = secret;
            JwtManage.expiredSecondConfig = expiredSecond;
        }

        /// <summary>
        /// 解析 jwt.
        /// </summary>
        /// <param name="token">jwt.</param>
        /// <typeparam name="TAccount"> <see cref="IAccount"/> 实现类型.</typeparam>
        /// <returns>account content.</returns>
        public static TAccount Parse<TAccount>(string token)
            where TAccount : class, new()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretConfig);
            _ = tokenHandler.ValidateToken(
                token.Split(' ').LastOrDefault(),
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // set clocks kew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    // TODO what?
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken);

            var jwtSecurityToken = (JwtSecurityToken) validatedToken;
            var account = new TAccount();

            foreach (var claim in jwtSecurityToken.Claims)
            {
                
            }

            return account;

            // .First(x => x.Type == nameof(CurrentUser.Code)).Value
        }

        /// <summary>
        /// 创建 jwt.
        /// </summary>
        /// <param name="account">账户内容.</param>
        /// <returns>jwt content.</returns>
        public static string Create(IAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretConfig);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(typeof(IAccount).GetProperties()
                    .Where(x => x.GetValue(account) != null)
                    .Select(x => new Claim(x.Name, x.GetValue(account)?.ToString() ?? string.Empty))
                    .ToArray()),
                Expires = DateTime.Now.AddHours(expiredSecondConfig),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return $"Bearer {tokenHandler.WriteToken(token)}";
        }
    }
}