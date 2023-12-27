using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Account;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistance.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);
                if (user is null)
            {
                user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
                if (user is null) throw new Exception("Username,Email or Password incorrect");
            }
                if(!await _userManager.CheckPasswordAsync(user,dto.Password)) throw new Exception("Username,Email or Password incorrect");
        }

        public async Task Register(RegisterDto dto)
        {

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dto.Email || u.UserName == dto.Username);
            if (user != null) throw new Exception("Account with this name already exists");
            user = _mapper.Map<AppUser>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }
                throw new Exception(sb.ToString());
            }

        }
    }
}
