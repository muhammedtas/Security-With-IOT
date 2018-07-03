using System.Threading.Tasks;
using SecurityWithIOT.API.Model;

namespace SecurityWithIOT.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> Exist(string username); 
         
    }
}