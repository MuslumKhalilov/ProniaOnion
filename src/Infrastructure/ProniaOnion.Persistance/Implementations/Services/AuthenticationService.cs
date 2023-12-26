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

        public AuthenticationService(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper= mapper;
        }
        public async Task Register(RegisterDto dto)
        {
            
            AppUser user =await _userManager.Users.FirstOrDefaultAsync(u=>u.Email==dto.Name ||u.Email==dto.Username);
            if (user != null) throw new Exception("Account with this name already exists");
            user = _mapper.Map<AppUser>(dto);
            var result= await _userManager.CreateAsync(user,dto.Password);
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
