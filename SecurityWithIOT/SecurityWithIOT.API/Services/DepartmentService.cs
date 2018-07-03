using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class DepartmentService:Repository<Department> , IDepartment
    {
        public DepartmentService(DataContext context) : base(context){

        }
    }
}