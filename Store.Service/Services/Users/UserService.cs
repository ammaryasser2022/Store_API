using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Store.Core.Dtos.Auth;
using Store.Core.Identity;
using Store.Core.Services.Contract;

namespace Store.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager ,ITokenService tokenService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

      

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user , loginDto.Password , false);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                //Token = // need to generate token 3shan el user da lma y3ml login ymshy beh betwenn pages so need service inject 
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            }; 
            
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //
           
            //



            //create fun in the service interface to check email fe zayo or not 
            // lw mfesh hbd2 a3ml var user mn appUser w azwd feh kol haga bt3t registrations


            if (await CheckEmailExistAsync(registerDto.Email)) return null;

            // lw tl3 false 
            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0],
                
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return null;


            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
                
            };

        }


        //Helper Method *
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null; // return true lw not null(t3ni l2et email zyo ) 
        }

        
        
        

    }
}
