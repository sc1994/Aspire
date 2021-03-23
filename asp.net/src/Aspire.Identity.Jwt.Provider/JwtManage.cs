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
    using Aspire.Logger;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Jwt Manage.
    /// </summary>
    internal class JwtManage
    {
        private readonly JwtAppSettings jwtAppSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtManage"/> class.
        /// </summary>
        /// <param name="jwtAppSettings">jwt app settings.</param>
        internal JwtManage(JwtAppSettings jwtAppSettings)
        {
            this.jwtAppSettings = jwtAppSettings;
        }

        /// <summary>
        /// Generate JwtToken.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>TokenDto.</returns>
        internal IdentityTokenDto GenerateJwtToken(ICurrentUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtAppSettings.Secret);
            DateTime expiryTime;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(typeof(ICurrentUser)
                    .GetProperties()
                    .Select(x => new Claim(x.Name, x.GetValue(user)?.ToString() ?? string.Empty))
                    .ToArray()),
                Expires = expiryTime = DateTime.Now.AddSeconds(this.jwtAppSettings.ExpireSeconds),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new IdentityTokenDto
            {
                Token = $"Bearer {tokenHandler.WriteToken(token)}",
                ExpiryTime = expiryTime,
                Ttl = this.jwtAppSettings.ExpireSeconds,
            };
        }

        /// <summary>
        /// Deconstruction JwtToken.
        /// </summary>
        /// <typeparam name="TCurrentUser">Current User.</typeparam>
        /// <param name="jwtToken">jwt token value.</param>
        /// <returns>Current User .</returns>
        internal ICurrentUser DeconstructionJwtToken<TCurrentUser>(string jwtToken)
            where TCurrentUser : ICurrentUser, new()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.jwtAppSettings.Secret);
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