using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.BL;
using EmployeeManagement.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.WebAPI.NewFolder;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Entities;



namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentBL departmentBL = new DepartmentBL();

        public DepartmentController()
        {

        }
        [HttpGet]
        [Authorize]

        public ActionResult<IEnumerable<Department>> GetDepartment()
        {
            return new ActionResult<IEnumerable<Department>>(departmentBL.GetDepartments());
        }
        [HttpGet("{deptid}")]
        [Authorize]
        public ActionResult<Department> GetDepartment(int deptid)
        {
            var Department = departmentBL.GetDepartment(deptid);

            if (Department == null)
            {
                return NotFound();
            }
            return Department;
        }
        [HttpPut("{deptid}")]
        [Authorize]

        public IActionResult PutDepartment(int deptid, Department1 department1)
        {
            Department department = new Department();
            department.DeptId = department1.DeptId;
            department.DeptName = department1.DeptName;


            if (deptid != department.DeptId)
            {
                return BadRequest();
            }
            try
            {
                departmentBL.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(deptid))
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
        public ActionResult<Department> PostDepartment(Department1 department1)
        {
            Department department = new Department();
            department.DeptId = department1.DeptId;
            department.DeptName = department1.DeptName;
            try
            {
                departmentBL.CreateDepartment(department);
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DeptId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetDepartment", new { id = department.DeptId }, department);

        }
        [HttpDelete("{deptid}")]
        [Authorize]
        public ActionResult<Department> DeleteDepartment(int deptid)
        {

            var Department = departmentBL.GetDepartment(deptid);
            if (Department == null)
            {
                return NotFound();
            }
            departmentBL.DeleteDepartment(Department.DeptId);

            return Department;
        }

        private bool DepartmentExists(int deptid)
        {
            if (departmentBL.GetDepartment(deptid)! == null)
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
