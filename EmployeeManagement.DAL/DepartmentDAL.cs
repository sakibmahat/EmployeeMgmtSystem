using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.EFCore.Models;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;



namespace EmployeeManagement.DAL
{
    public class DepartmentDAL
    {
        EmployeeManagementContext objEmployeeManagementContext= new EmployeeManagementContext();

        public void CreateDepartment(Department objDepartment)
        {
            objEmployeeManagementContext.Add(objDepartment);
            objEmployeeManagementContext.SaveChanges();
        }


        public void UpdateDepartment(Department objdepartment)
        {
            objEmployeeManagementContext.Entry(objdepartment).State =Microsoft.EntityFrameworkCore.EntityState.Modified;
            objEmployeeManagementContext.SaveChanges();
        }

        public void DeleteDepartment(int  deptid)
        {
            Department objDepartment = objEmployeeManagementContext.Departments.Find(deptid);
            objEmployeeManagementContext.Remove(objDepartment);
            objEmployeeManagementContext.SaveChanges();
        }
        public Department GetDepartment(int deptid)
        {
            Department objdepartment =objEmployeeManagementContext.Departments.Find(deptid);
            return objdepartment;
        }
        public IEnumerable<Department> GetDepartment()
        {
            return objEmployeeManagementContext.Departments;
        }
    }
}
