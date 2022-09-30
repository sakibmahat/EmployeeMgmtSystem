namespace EmployeeManagement.WebAPI.NewFolder
{
    public class Employee1
    {

        public string  EmpId { get; set; } = null!;
        public string EmpFirstName { get; set; } = null!;
        public string? EmpLastName { get; set; }
        public DateTime EmpDateOfBirth { get; set; }
        public DateTime EmpDateOfJoining { get; set; }
        public int EmpDeptId { get; set; }
        public string EmpGradeCode { get; set; } = null!;
        public string? EmpDesignation { get; set; }
        public int EmpBasicSalary { get; set; }
        public string EmpGender { get; set; } = null!;
        public string EmpMartialStatus { get; set; } = null!;
        public string? EmpHomeAddress { get; set; }
        public string? EmpContactNum { get; set; }


    }
}
