
using  EmployeeManagement.DAL;
using  EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.EFCore.Models;

namespace EmployeeManagement.BL
{
    public class DepartmentBL
    {
        DepartmentDAL objDepartmentDAL = new DepartmentDAL();

        public void CreateDepartment(Department objDepartment)
        {
            objDepartmentDAL.CreateDepartment(objDepartment);
        }

        public void UpdateDepartment(Department objDepartment)
        {
            objDepartmentDAL.UpdateDepartment(objDepartment);
        }

        public void DeleteDepartment(int deptid)
        {
            objDepartmentDAL.DeleteDepartment(deptid);
        }

        public Department GetDepartment(int deptid)
        {
            Department objDepartment = objDepartmentDAL.GetDepartment(deptid);
            return objDepartment;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return objDepartmentDAL.GetDepartment();
        }
    }
}


