// <copyright file="JwtManage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity.Jwt.Provider
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Aspire.Identities;
    using Aspire.Identity.Jwt.Provider.AppSettings;
    using Aspire.Loggers;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Jwt Manage.
    /// </summary>
    internal class JwtManage
    {
        /// <summary>
        /// Generate JwtToken.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="appSetting">App Setting.</param>
        /// <returns>Identity TokenDto.</returns>
        internal static IdentityTokenDto GenerateJwtToken(ICurrentUser user, IdentityAppSetting appSetting)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);
            DateTime expiryTime;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(typeof(ICurrentUser)
                    .GetProperties()
                    .Select(x => new Claim(x.Name, x.GetValue(user)?.ToString() ?? string.Empty))
                    .ToArray()),
                Expires = expiryTime = DateTime.Now.AddSeconds(appSetting.ExpireSeconds),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new IdentityTokenDto
            {
                Token = $"Bearer {tokenHandler.WriteToken(token)}",
                ExpiryTime = expiryTime,
                Ttl = appSetting.ExpireSeconds,
                TokenHeaderName = "Authorization",
            };
        }

        /// <summary>
        /// Deconstruction JwtToken.
        /// </summary>
        /// <typeparam name="TCurrentUser">Current User.</typeparam>
        /// <param name="jwtToken">jwt token value.</param>
        /// <param name="appSetting">App Setting.</param>
        /// <returns>Current User .</returns>
        internal static ICurrentUser DeconstructionJwtToken<TCurrentUser>(string jwtToken, IdentityAppSetting appSetting)
            where TCurrentUser : ICurrentUser, new()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSetting.Secret);
                _ = tokenHandler.ValidateToken(
                    jwtToken.Split(' ').LastOrDefault(),
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        // set clocks kew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        // TODO what?
                        ClockSkew = TimeSpan.Zero,
                    },
                    out var validatedToken);

                var token = (JwtSecurityToken)validatedToken;
                return new TCurrentUser
                {
                    Account = token.Claims.First(x => x.Type == nameof(ICurrentUser.Account)).Value,
                    Name = token.Claims.First(x => x.Type == nameof(ICurrentUser.Name)).Value,
                    Roles = token.Claims.First(x => x.Type == nameof(ICurrentUser.Roles)).Value.DeserializeObject<string[]>(),
                };
            }
            catch (Exception ex)
            {
                if (ex is not SecurityTokenExpiredException)
                {
                    ServiceLocator.ServiceProvider
                        .GetService<ILogWriter>()
                        .Error(ex, "Jwt Token Exception");
                    throw FriendlyThrowException.ThrowException(ResponseCode.AuthorizeInvalid);
                }

                throw FriendlyThrowException.ThrowException(ResponseCode.AuthorizeExpired);
            }
        }
    }
}