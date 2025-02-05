using CoNetic.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Core.ServicesInterfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User User,UserManager<User> userManager);
    }
}
