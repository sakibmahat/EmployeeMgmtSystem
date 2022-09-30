using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities
{
    public partial class GradeMaster
    {
        public GradeMaster()
        {
            Employees = new HashSet<Employee>();
        }
        [Required]
        public string GradeCode { get; set; } = null!;
        [Required]
        public string Designation { get; set; } = null!;
        [Required]
        public int MinSalary { get; set; }
        [Required]
        public int MaxSalary { get; set; }
        [Required]

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
