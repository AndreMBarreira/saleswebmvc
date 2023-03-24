using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SaleWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size shold be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BithDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Base Salary")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public int DepartmentId { get; set; }
        
        [ValidateNever]
        public Department Department { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(string name, string email, DateTime bithDate, double baseSalary, int departmentId, Department department)
        {
            Name = name;
            Email = email;
            BithDate = bithDate;
            BaseSalary = baseSalary;
            DepartmentId = departmentId;
            Department = department;
        }

        public Seller(int id, string name, string email, DateTime bithDate, double baseSalary, int departmentId, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BithDate = bithDate;
            BaseSalary = baseSalary;
            DepartmentId = departmentId;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final)
                .Sum(sr => sr.Amount);
        }
    }
}
