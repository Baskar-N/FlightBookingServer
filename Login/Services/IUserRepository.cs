using LoginApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApiService.Services
{
    public interface IUserRepository
    {
        User Register(User user);

        User Login(User user);

        public UserToken GetToken(string token);

        UserToken RefreshToken(UserToken user);
    }
}
