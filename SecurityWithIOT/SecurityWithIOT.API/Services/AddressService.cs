using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class AddressService : Repository<Address> , IAddress
    {
        public AddressService(DataContext context) : base(context){

        }
    }
}