using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Aspire.Jwt
{
    /// <summary>
    /// jwt 管理类.
    /// </summary>
    public static class JwtManage
    {
        private static string secret;
        private static int expiredSecond;

        /// <summary>
        /// 初始化 jwt 需要的内容.
        /// </summary>
        /// <param name="secret">密钥.</param>
        /// <param name="expiredSecond">失效时间(秒).</param>
        public static void InitJwtManage(string secret, int expiredSecond)
        {
            JwtManage.secret = secret;
            JwtManage.expiredSecond = expiredSecond;
        }

        /// <summary>
        /// 解析 jwt.
        /// </summary>
        /// <param name="token">jwt.</param>
        /// <returns>account content.</returns>
        public static ICurrentAccount Parse(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
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
            return new CurrentUser(
                jwtSecurityToken.Claims.First(x => x.Type == nameof(CurrentUser.WorkNo)).Value,
                jwtSecurityToken.Claims.First(x => x.Type == nameof(CurrentUser.Code)).Value,
                jwtSecurityToken.Claims.First(x => x.Type == nameof(CurrentUser.Name)).Value,
                jwtSecurityToken.Claims.First(x => x.Type == nameof(CurrentUser.Avatar)).Value
            );
        }

        /// <summary>
        /// 创建 jwt.
        /// </summary>
        /// <param name="account">账户内容.</param>
        /// <returns>jwt content.</returns>
        public static string Create(ICurrentAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(typeof(ICurrentAccount).GetProperties()
                    .Where(x => x.GetValue(account) != null)
                    .Select(x => new Claim(x.Name, x.GetValue(account)?.ToString() ?? string.Empty))
                    .ToArray()),
                Expires = DateTime.Now.AddHours(expiredSecond),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return $"Bearer {tokenHandler.WriteToken(token)}";
        }
    }
}