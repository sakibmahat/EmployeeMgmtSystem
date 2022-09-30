using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.EFCore.Models;
using EmployeeManagement.Entities;

using EmployeeManagement.DAL;



namespace EmployeeManagement.DAL
{
    public class GradeMasterDAL
    {
        EmployeeManagementContext objEmployeeManagementContext = new EmployeeManagementContext();

        public void CreateGradeMaster(GradeMaster objGradeMaster)
        {
            objEmployeeManagementContext.Add(objGradeMaster);
            objEmployeeManagementContext.SaveChanges();
        }


        public void UpdateGradeMaster(GradeMaster objGradeMaster)
        {
            objEmployeeManagementContext.Entry(objGradeMaster).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            objEmployeeManagementContext.SaveChanges();
        }

        public void DeleteGradeMaster(string GradeCode)
        {
            GradeMaster objGradeMaster = objEmployeeManagementContext.GradeMasters.Find(GradeCode);
            objEmployeeManagementContext.Remove(objGradeMaster);
            objEmployeeManagementContext.SaveChanges();
        }
        public GradeMaster GetGradeMaster(string GradeCode)
        {
            GradeMaster objGradeMaster = objEmployeeManagementContext.GradeMasters.Find(GradeCode);
            return objGradeMaster;
        }
        public IEnumerable<GradeMaster> GetGradeMaster()
        {
            return objEmployeeManagementContext.GradeMasters;
        }
    }
}
