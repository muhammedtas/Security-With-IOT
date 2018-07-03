using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class CompanyService :Repository<Company> , ICompany
    {
        public CompanyService(DataContext context) : base(context){

        }
    }
}