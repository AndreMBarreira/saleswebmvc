using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
