using EmployeeManagement.Entities;
using System;
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
    public class GradeMasterController : ControllerBase
    {
        private readonly GradeMasterBL grademasterBL = new GradeMasterBL();

        public GradeMasterController()
        {

        }
        [HttpGet]
        [Authorize]

        public ActionResult<IEnumerable<GradeMaster>> GetGradeMaster()
        {
            return new ActionResult<IEnumerable<GradeMaster>>(grademasterBL.GetGradeMasters());
        }
        [HttpGet("{GradeCode}")]
        [Authorize]
        public ActionResult<GradeMaster> GetGradeMaster(string GradeCode)
        {
            var GradeMaster = grademasterBL.GetGradeMaster(GradeCode);

            if (GradeMaster == null)
            {
                return NotFound();
            }
            return GradeMaster;
        }
        [HttpPut("{GradeCode}")]
        [Authorize]

        public IActionResult putGradeMaster(string gradecode, GradeMaster1 grademaster1)
        {
          
            GradeMaster grademaster = new GradeMaster();
            grademaster.MaxSalary = grademaster1.MaxSalary;
            grademaster.MinSalary = grademaster1.MinSalary;
            grademaster.GradeCode = grademaster1.GradeCode;
            grademaster.Designation = grademaster1.Designation;

            if (gradecode != grademaster.GradeCode)
            {
                return BadRequest();
            }
            try
            {
                grademasterBL.UpdateGradeMaster(grademaster);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeMasterExists(gradecode))
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
        public ActionResult<GradeMaster> PostGradeMaster(GradeMaster1 grademaster1)
        {
            GradeMaster grademaster = new GradeMaster();
            grademaster.MaxSalary = grademaster1.MaxSalary;
            grademaster.MinSalary = grademaster1.MinSalary;
            grademaster.GradeCode = grademaster1.GradeCode;
            grademaster.Designation = grademaster1.Designation;
            try
            {
                grademasterBL.CreateGradeMaster(grademaster);
            }
            catch (DbUpdateException)
            {
                if (GradeMasterExists(grademaster.GradeCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetGradeMaster", new { id = grademaster.GradeCode }, grademaster);

        }
        [HttpDelete("{GradeCode}")]
        [Authorize]
        public ActionResult<GradeMaster> DeleteGradeMaster(string GradeCode)
        {

            var GradeMaster = grademasterBL.GetGradeMaster(GradeCode);
            if (GradeMaster == null)
            {
                return NotFound();
            }
            grademasterBL.DeleteGradeMaster(GradeMaster.GradeCode);

            return GradeMaster;
        }

        private bool GradeMasterExists(string GradeCode)
        {
            if (grademasterBL.GetGradeMaster(GradeCode) != null)
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

