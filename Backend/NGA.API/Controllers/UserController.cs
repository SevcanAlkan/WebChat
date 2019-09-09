using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.API.Controllers
{
    public class UserController : DefaultApiController<UserAddVM, UserUpdateVM, UserVM, IUserService>
    {
        private IConfiguration _config;

        public UserController(IUserService service, IConfiguration config)
             : base(service)
        {
            _config = config;
        }

        [AllowAnonymous]
        public async Task<JsonResult> Login(UserLoginVM model)
        {
            APIResultVM result = await _service.Authenticate(model.UserName, model.PasswordHash);

            var signingKey = System.Convert.FromBase64String(_config["Jwt:Key"]);
            var expiryDuration = int.Parse(_config["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim> {
                new Claim("userid", (result.Rec as UserVM).Id.ToString())
            }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);

            return new JsonResult(token);
        }

        public override JsonResult Get()
        {
            return base.Get();
        }

        public override Task<JsonResult> GetById(Guid id)
        {
            return base.GetById(id);
        }

        public override Task<JsonResult> Add(UserAddVM model)
        {
            return base.Add(model);
        }

        public override Task<JsonResult> Update(Guid id, UserUpdateVM model)
        {
            return base.Update(id, model);
        }

        public override Task<JsonResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}