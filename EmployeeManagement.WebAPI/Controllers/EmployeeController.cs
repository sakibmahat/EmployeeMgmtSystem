using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.BL;
using EmployeeManagement.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.WebAPI.NewFolder;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeBL employeeBL = new EmployeeBL();

        public EmployeeController()
        {

        }
        [HttpGet]
        [Authorize]

        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            return new ActionResult<IEnumerable<Employee>>(employeeBL.GetEmployees());
        }
        [HttpGet("{empid}")]
        [Authorize]
        public ActionResult<Employee> GetEmployee(string empid)
        {
            var Employee = employeeBL.GetEmployee(empid);

            if (Employee == null)
            {
                return NotFound();
            }
            return Employee;
        }
       
        [HttpPut("{empid}")]
        [Authorize]

        public IActionResult putEmployee(string empid, Employee1 employee1)
        {
            Employee employee = new Employee();
            employee.EmpId = employee1.EmpId;
            employee.EmpFirstName = employee1.EmpFirstName;
            employee.EmpLastName = employee1.EmpLastName;
            employee.EmpDateOfBirth = employee1.EmpDateOfBirth;
            employee.EmpDateOfJoining = employee1.EmpDateOfJoining;
            employee.EmpDeptId = employee1.EmpDeptId;
            employee.EmpGradeCode = employee1.EmpGradeCode;
            employee.EmpDesignation = employee1.EmpDesignation;
            employee.EmpBasicSalary = employee1.EmpBasicSalary;
            employee.EmpGender = employee1.EmpGender;
            employee.EmpMartialStatus = employee1.EmpMartialStatus;
            employee.EmpHomeAddress = employee1.EmpHomeAddress;
            employee.EmpContactNum = employee1.EmpContactNum;
            

                    
           if(empid != employee.EmpId)
           
            {
                return BadRequest();
            }
            try
            {
                employeeBL.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(empid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpPost]
        [Authorize]
        public ActionResult<Employee> PostEmployee(Employee1 employee1)
        {
            Employee employee = new Employee();
            employee.EmpId = employee1.EmpId;
            employee.EmpFirstName = employee1.EmpFirstName;
            employee.EmpLastName = employee1.EmpLastName;
            employee.EmpDateOfBirth = employee1.EmpDateOfBirth;
            employee.EmpDateOfJoining = employee1.EmpDateOfJoining;
            employee.EmpDeptId = employee1.EmpDeptId;
            employee.EmpGradeCode = employee1.EmpGradeCode;
            employee.EmpDesignation = employee1.EmpDesignation;
            employee.EmpBasicSalary = employee1.EmpBasicSalary;
            employee.EmpGender = employee1.EmpGender;
             employee.EmpMartialStatus = employee1.EmpMartialStatus;
            employee.EmpHomeAddress = employee1.EmpHomeAddress;
            employee.EmpContactNum = employee1.EmpContactNum;

            try
            {
                employeeBL.CreateEmployee(employee);
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmpId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetEmployee", new { id = employee.EmpId }, employee);

        }
        [HttpDelete("{empid}")]
        [Authorize]
        public ActionResult<Employee> DeleteEmployee(string empid)
        {

            var Employee = employeeBL.GetEmployee(empid);
            if (Employee == null)
            {
                return NotFound();
            }
            employeeBL.DeleteEmployee(Employee.EmpId);

            return Employee;
        }

        private bool EmployeeExists(string empid)
        {
            if (employeeBL.GetEmployee(empid)! == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


