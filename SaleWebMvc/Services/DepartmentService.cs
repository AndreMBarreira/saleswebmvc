using SaleWebMvc.Data;
using SaleWebMvc.Models;

namespace SaleWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SaleWebMvcContext _context;

        public DepartmentService(SaleWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();

        }
    }
}
