﻿using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoHub.Entities;

namespace VideoHub.ServiceApi.Utils
{
    public interface IJwtHelper
    {
        string GetToken(User user);
    }
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IHttpContextAccessor _ihttpContextAccessor;
        public JwtHelper(IConfiguration iconfiguration, IHttpContextAccessor ihttpContextAccessor)
        {
            _iconfiguration = iconfiguration;
            _ihttpContextAccessor = ihttpContextAccessor;
        }
        public string GetToken(User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("参数不能为空");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _iconfiguration.GetValue<string>("JwtKey");
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.LoginName),
                    new Claim(JwtClaimTypes.Role, user.Role.ToString()),
                    //new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.NickName, user.NickName),
                    //new Claim(JwtClaimTypes.Picture, user.HeadImageSrc),
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber)
                    //new Claim(JwtClaimTypes.Audience,"api"),
                    //new Claim(JwtClaimTypes.Issuer,"VideoHub")
                }),
                Expires = expiresAt,
                Issuer = "api",
                Audience = "VideoHub",
                NotBefore = authTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public User GetCurrentUser()
        {
            if (_ihttpContextAccessor.HttpContext.User.Identity.IsAuthenticated) {
                var userInfo = new User();
                userInfo.LoginName = _ihttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == JwtClaimTypes.Name).Value;
                userInfo.Id = Convert.ToInt32(_ihttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == JwtClaimTypes.Id).Value);
                userInfo.Role = Convert.ToInt32(_ihttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == JwtClaimTypes.Role).Value);
                return userInfo;
            }
            return null;
        }
    }
}
