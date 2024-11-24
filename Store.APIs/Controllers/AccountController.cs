using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Error;
using Store.APIs.Exentions;
using Store.Core.Dtos.Auth;
using Store.Core.Identity;
using Store.Core.Services.Contract;

namespace Store.APIs.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService , UserManager<AppUser> userManager , ITokenService tokenService , IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")] //BaseUrl/api/Acount/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await _userService.LoginAsync(loginDto);
            if (user == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized , "Invalid Login"));

            return Ok(user);
        }

        [HttpPost("register")] //BaseUrl/api/Acount/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            
            var user = await _userService.RegisterAsync(registerDto);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , "Invalid Registration :(") );

            return Ok(user);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]  
        public async Task<ActionResult<UserDto>> GetCurrentUser() // msh hta5d haga 3ayzha tgeb info elly 3amel login noww fa lazem Authoriz
        {
            // User de inherit from Controller base -- have claims to the user whoes login now if there he login 
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok (new UserDto() 
            {
                Email = Email, 
                DisplayName = user.DisplayName, 
                Token = await _tokenService.CreateTokenAsync(user , _userManager)
            });
        }


        [Authorize]

        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress() //
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            //var user = await _userManager.FindByEmailAsync(Email); //Note this fun dont include property of address :( create another
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var returnedAddress = _mapper.Map<AddressDto>(user.Address); 

            return Ok(returnedAddress); 
        }

        [Authorize]

        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress([FromBody] AddressDto address) //
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            //var user = await _userManager.FindByEmailAsync(Email); //Note this fun dont include property of address :( create another
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));


            user.Address.FName = address.FName;
            user.Address.LName = address.LName;
            user.Address.City = address.City;
            user.Address.Street = address.Street;
            user.Address.Country = address.Country;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest  , "Unable to update adress :("));

            }

            var updatedAddress = _mapper.Map<AddressDto>(user.Address);

            return Ok(updatedAddress);
        }



        //[HttpGet("emailExist")]
        //public async Task<bool> CheckEmailExistAsync(string Email)
        //{
        //    //var user = await _userManager.FindByEmailAsync(Email);

        //    //if (user == null) return false;

        //    //return true;


        //    // Another Way Syntax Suger 
        //     return await _userManager.FindByEmailAsync(Email) is not null;  


        //}


    }
}
