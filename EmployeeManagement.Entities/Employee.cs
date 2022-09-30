using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManagement.Entities
{
    public partial class Employee
    {
        public string EmpId { get; set; } = null!;
        [Required ]
        public string EmpFirstName { get; set; } = null!;
        [Required]
        public string EmpLastName { get; set; } = null!;
        [Required]
        public DateTime EmpDateOfBirth { get; set; }
        [Required]
        public DateTime EmpDateOfJoining { get; set; }
        [Required]
        public int EmpDeptId { get; set; }
        [Required]
        public string EmpGradeCode { get; set; } = null!;
        [Required]
        public string EmpDesignation { get; set; } = null!;
        [Required]
        public int EmpBasicSalary { get; set; }
        [Required]
        public string EmpGender { get; set; } = null!;
        [Required]
        public string EmpMartialStatus { get; set; } = null!;
        [Required]
        public string EmpHomeAddress { get; set; } = null!;
        [Required]
        public string EmpContactNum { get; set; } = null!;
        [Required]

        public virtual Department EmpDept { get; set; } = null!;
        [Required]
        public virtual GradeMaster EmpGradeCodeNavigation { get; set; } = null!;
    }
}
