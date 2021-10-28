using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Aspire.Helpers
{
    /// <summary>
    /// Json Web Token.
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// 生成 jwt.
        /// </summary>
        /// <param name="data">数据.</param>
        /// <param name="expirationSeconds">过期 秒.</param>
        /// <param name="secret">密钥.</param>
        /// <typeparam name="T">data 的类型.</typeparam>
        /// <returns>jwt.</returns>
        public static string GenerateJwt<T>(
            T data,
            int expirationSeconds,
            string secret)
            where T : class
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("data", data.ToJsonString()) }),
                Expires = DateTime.UtcNow.AddSeconds(expirationSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return "Bearer " + tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 解析 jwt.
        /// </summary>
        /// <param name="jwt">jwt字符串.</param>
        /// <param name="secret">密钥.</param>
        /// <typeparam name="T">解析后的数据类型.</typeparam>
        /// <returns>解析的数据内容.</returns>
        public static T ParseJwt<T>(string jwt, string secret)
            where T : class
        {
            if (!jwt.StartsWith("Bearer "))
            {
                throw new ArgumentException("jwt 应该为 Bearer 开头");
            }

            jwt = jwt.Substring("Bearer ".Length);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            tokenHandler.ValidateToken(
                jwt,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                },
                out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            if (jwtToken is null) throw new NullReferenceException(nameof(jwtToken));

            var firstClaim = jwtToken.Claims.FirstOrDefault();
            if (firstClaim is null) throw new NullReferenceException(nameof(firstClaim));

            return firstClaim.Value.ToObjByJson<T>() ?? throw new NullReferenceException("jwt payload json to data exception");
        }
    }
}