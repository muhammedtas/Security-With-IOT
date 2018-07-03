using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityWithIOT.API.Data;

namespace SecurityWithIOT.API.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentsController : Controller
    {
        

        public DataContext _context { get; }

        public DepartmentsController(DataContext context){
            _context = context;
        }
        // GET api/departmens
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _context.Departments.ToListAsync();
            return Ok(value);
        }

    }
}