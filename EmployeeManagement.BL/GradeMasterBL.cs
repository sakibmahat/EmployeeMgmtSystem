
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
    public class GradeMasterBL
    {
        GradeMasterDAL objGradeMasterDAL = new GradeMasterDAL();

        public void CreateGradeMaster(GradeMaster objGradeMaster)
        {
            objGradeMasterDAL.CreateGradeMaster(objGradeMaster);
        }

        public void UpdateGradeMaster(GradeMaster objGradeMaster)
        {
            objGradeMasterDAL.UpdateGradeMaster(objGradeMaster);
        }

        public void DeleteGradeMaster(string GradeCode)
        {
            objGradeMasterDAL.DeleteGradeMaster(GradeCode);
        }

        public GradeMaster GetGradeMaster(string GradeCode)
        {
            GradeMaster objGradeMaster = objGradeMasterDAL.GetGradeMaster(GradeCode);
            return objGradeMaster;
        }

        public IEnumerable<GradeMaster> GetGradeMasters()
        {
            return objGradeMasterDAL.GetGradeMaster();
        }
    }
}




