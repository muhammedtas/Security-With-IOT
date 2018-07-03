using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class UserService : Repository<User> , IUser
    {
        public UserService(DataContext context) : base(context){

        }

    }
}