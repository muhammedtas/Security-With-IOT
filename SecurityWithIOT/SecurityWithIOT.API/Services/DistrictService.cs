using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class DistrictService: Repository<District> , IDistrict
    {
        public DistrictService(DataContext context) : base(context){

        }

    }
}