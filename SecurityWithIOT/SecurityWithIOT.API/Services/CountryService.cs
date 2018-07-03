using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class CountryService: Repository<Country> , ICountry
    {
        public CountryService(DataContext context) : base(context){

        }

    }
}