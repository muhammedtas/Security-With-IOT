using System;
using System.Threading.Tasks;
using SecurityWithIOT.API.Model;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SecurityWithIOT.API.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SecurityWithIOT.API.Data
{
    [Authorize]
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;
        private readonly IUser _userService;

        public AuthRepository(DataContext dbContext, IUser userService)
        {
            this._dbContext = dbContext;
            _userService = userService;
        }

        public async Task<bool> Exist(string username)
        {
            //if(await _dbContext.Users.AnyAsync(x=>x.Username == username)) return true;
            if(await _userService.GetAll().AnyAsync(x=>x.Username == username)) return true;

            return false;
        }

        public async Task<User> Register(User user, string password)
        {       
            try
            {
                 byte[] passwordHash,passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                // await _dbContext.Users.AddAsync(user);
                // await _dbContext.SaveChangesAsync();
                await _userService.AddAsync(user);
                await _userService.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
               
            return user;
        }
        public async Task<User> Login(string username, string password)
        {
            //var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            var user = await _userService.GetAll().Include( p => p.Photos).FirstOrDefaultAsync(x=>x.Username == username); // 114

            if(user ==null) return null;

            if(!VerifyUserPasswordHash(password,user.PasswordHash,user.PasswordSalt)) return null;
        
            return user;
        }
     

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                        passwordSalt = hmac.Key;
                        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }

         private bool VerifyUserPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

                using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
                {
                        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                        for(int i = 0; i < computedHash.Length; i++ ){

                            if(computedHash[i] != passwordHash[i]) return false; // Burası return false olacak. Ancak hash te bi sıkıntı var devam ediyoruz şimdilik.
                        }
                }

            return true;
        }

    }
}