using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Core.Dtos.Auth;

namespace Store.Core.Services.Contract
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto); //login need email and pass so o will create a DTO fothem // and return need Dto has token username , email

        Task<UserDto> RegisterAsync(RegisterDto  registerDto);

        Task<bool> CheckEmailExistAsync(string email);

    }
}
