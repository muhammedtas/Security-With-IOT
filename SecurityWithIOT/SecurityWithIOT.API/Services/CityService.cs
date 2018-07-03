using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class CityService: Repository<City> , ICity
    {
        public CityService(DataContext context) : base(context){

        }

    }
}