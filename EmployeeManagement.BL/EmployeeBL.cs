using EmployeeManagement.DAL;
using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.EFCore.Models;

namespace EmployeeManagement.BL
{
    public class EmployeeBL
    {
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();

        public void CreateEmployee(Employee objEmployee)
        {
            objEmployeeDAL.CreateEmployee(objEmployee);
        }

        public void UpdateEmployee(Employee objEmployee)
        {
            objEmployeeDAL.UpdateEmployee(objEmployee);
        }

        public void DeleteEmployee(string empid)
        {
            objEmployeeDAL.DeleteEmployee(empid);
        }

        public Employee GetEmployee(string empid)
        {
            Employee objEmployee = objEmployeeDAL.GetEmployee(empid);
            return objEmployee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return objEmployeeDAL.GetEmployee();
        }
    }
}



