using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Core.Validation;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.API.Controllers
{
    public class UserController : DefaultApiController
    {
        private IConfiguration _config;
        private IUserService _service;
        private readonly IMapper _mapper;

        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public UserController(IUserService service, IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _config = config;
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        public JsonResult UserNameIsExist(string userName)
        {
            try
            {
                var result = _service.Any(userName);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(APIResult.CreateVMWithError(ex, APIResult.CreateVM(false, null, AppStatusCode.ERR01001)));
            }
        }

        public JsonResult Get()
        {
            try
            {
                var result = _service.GetUserList();

                if (result == null)
                    return new JsonResult(APIResult.CreateVM(false, null, AppStatusCode.WRG01001));

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(APIResult.CreateVMWithError(ex, APIResult.CreateVM(false, null, AppStatusCode.ERR01001)));
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> CreateToken(UserLoginVM model)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
                return new JsonResult(APIResult.CreateVM(false, null, AppStatusCode.WRG01004));

            var user = await _userManager.FindByNameAsync(model.UserName);


            UserAuthenticateVM returnVM = new UserAuthenticateVM();
            returnVM = _mapper.Map<User, UserAuthenticateVM>(user);
            returnVM.Token = GetToken(user);

            return new JsonResult(returnVM);
        }

        public async Task<JsonResult> RefreshToken()
        {
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );

            return new JsonResult(APIResult.CreateVMWithRec<string>(GetToken(user), true));
        }

        [AllowAnonymous]
        public async Task<JsonResult> Register(UserAddVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User entity = _mapper.Map<UserAddVM, User>(model);
                    entity.Id = Guid.NewGuid();
                    var identityResult = await _userManager.CreateAsync(entity, model.PasswordHash);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(entity, isPersistent: false);


                        UserAuthenticateVM returnVM = new UserAuthenticateVM();
                        returnVM = _mapper.Map<User, UserAuthenticateVM>(entity);
                        returnVM.Token = GetToken(entity);

                        return new JsonResult(returnVM);
                    }
                    else
                    {
                        return new JsonResult(APIResult.CreateVMWithRec<object>(identityResult.Errors));
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(APIResult.CreateVMWithError(ex, APIResult.CreateVM(false, null, AppStatusCode.ERR01001)));                
            }
            return new JsonResult(APIResult.CreateVM(false, null, AppStatusCode.ERR01001));
        }

        private String GetToken(User user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("Jwt:Key")));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_config.GetValue<int>("Jwt:ExpiryDuration")),
                audience: _config.GetValue<String>("Jwt:Audience"),
                issuer: _config.GetValue<String>("Jwt:Issuer")
                );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
