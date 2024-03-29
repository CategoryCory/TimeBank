﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> CreateToken(ApplicationUser user)
        {
            var claims = await GetClaims(user);

            var credentials = GetSigningCredentials();

            //var tokenOptions = new JwtSecurityToken(
            //    issuer: _config.GetSection("JwtValidIssuer").Get<string>(),
            //    audience: _config.GetSection("JwtValidAudience").Get<string>(),
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(_config.GetSection("JwtExpiresInDays").Get<int>()),
            //    signingCredentials: credentials
            //);

            var tokenOptions = new JwtSecurityToken(
                issuer: _config["JwtSettings:ValidIssuer"],
                audience: _config["JwtSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(_config.GetSection("JwtSettings:ExpiresInMinutes").Get<int>()),
                signingCredentials: credentials
            );


            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            //var tokenKey = Encoding.UTF8.GetBytes(_config.GetSection("JwtSecurityKey").Get<string>());
            var tokenKey = Encoding.UTF8.GetBytes(_config["JwtSettings:SecurityKey"]);
            var securityKey = new SymmetricSecurityKey(tokenKey);

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
