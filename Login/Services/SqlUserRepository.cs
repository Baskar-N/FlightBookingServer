using LoginApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApiService.Services
{
    public class SqlUserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;

        public SqlUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // validate user credentials
        public User Login(User user)
        {
            var loggedUser = _appDbContext.User.FirstOrDefault(item => 
                                    item.EmailId == user.EmailId
                                    && item.IsActive
                                );

            return loggedUser;
        }

        // get the token details
        public UserToken GetToken(string token)
        {
            //var tokenData = _appDbContext.UserToken.FirstOrDefault(item => item.Token == token);
            var tokenData = (from u in _appDbContext.UserToken
                            where u.Token == token
                            select new UserToken
                            {
                                TokenId = u.TokenId,
                                Token = u.Token,
                                UserRecId = u.UserRecId,
                                Expires = u.Expires,
                                Created = u.Created,
                                User = u.User
                            }).FirstOrDefault();

            return tokenData;
        }

        // update the cookie token value
        public UserToken RefreshToken(UserToken user)
        {
            var token = GetToken(user.Token);

            if(token == null)
            {
                _appDbContext.UserToken.Add(user);
            }
            else
            {
                token.Token = user.Token;
                token.Expires = user.Expires;
                token.Created = user.Created;
            }

            _appDbContext.SaveChanges();

            return token;
        }

        // register the new user.
        public User Register(User user)
        {
            _appDbContext.User.Add(user);
            _appDbContext.SaveChanges();

            return user;
        }
    }
}
