using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace EmployeeManagement.MVC.Controllers

{
    public class EmployeeMVCController : Controller
    {
        public EmployeeMVCController()
        {

        }
        /* string baseUrl;
         public EmployeeMVCController(IConfiguration configuration)
         {
             baseUrl = configuration["APIbaseurl"];
         }*/

        public async Task<IActionResult> Index()
        {
            try
            {

                List<Employee> objEmployees = await GetEmployees();

                return View(objEmployees);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(string? empid)
        {
            if (empid == null)
            {
                return NotFound();
            }
            try
            {


                var Employee = await GetEmployee(empid);

                if (Employee == null)
                {
                    return NotFound();
                }

                return View(Employee);
            }
            catch
            {
                
                if (empid == null)
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpFirstName,EmpLastName,EmpDeptId,EmpDateOfBirth,EmpDateOfJoining,EmpGradeCode,EmpDesignation,EmpBasicSalary,EmpGender,EmpMartialStatus,EmpHomeAddress,EmpContactNum")] Employee employee)
        {
            try
            {

                await AddEmployee(employee);
            }
            catch (Exception)
            {
                throw;
            }
            return View(employee);
        }
     //
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string empid, [Bind("EmpId,EmpFirstName,EmpLastName,EmpDeptId,EmpDateOfBirth,EmpDateOfJoining,EmpGradeCode,EmpDesignation,EmpBasicSalary,EmpGender,EmpMartialStatus,EmpHomeAddress,EmpContactNum")] Employee employee)
        {
            if (empid != employee.EmpId)
            {
                return NotFound();
            }

            try
            {
                await UpdateEmployee(employee);

            }
            catch (Exception)
            {
                if (EmployeeExists(employee.EmpId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }


        // GET: GradeMastersMVC/Delete/5

        public async Task<IActionResult> Delete(string? empid)
        {
            if (empid == null)
            {
                return NotFound();
            }

            try
            {


                Employee objEmployee = await GetEmployee(empid);

                if (objEmployee == null)
                {
                    return NotFound();
                }

                return View(objEmployee);
            }
            catch
            {
                throw;
            }
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
      
        private async Task<Employee> EmployeeExists(string empid)
        {
            //return _context.GradeMasters.Any(e => e.Id == id);
            Employee objEmployee = await GetEmployee(empid);
            return objEmployee;

        }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                string baseUrl = "https://localhost:7039";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

              /*  HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);*/



                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);




                HttpResponseMessage response = await client.GetAsync("/api/Employee");

                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Employee> data = JsonConvert.DeserializeObject<List<Employee>>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {
                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Employee> GetEmployee(string? empid)
        {
            try
            {


                string baseUrl = "https://localhost:7039";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                HttpResponseMessage response = await client.GetAsync("/api/Employee/" + empid);


                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Employee data = JsonConvert.DeserializeObject<Employee>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Employee> AddEmployee(Employee objEmployee)
        {
            try
            {


                string baseUrl = "https://localhost:7039";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);





                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                string stringData = JsonConvert.SerializeObject(objEmployee);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Employee/", contentData);



                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objEmployee;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<Employee> UpdateEmployee(Employee objEmployee)
        {

            try
            {

                string baseUrl = "https://localhost:7039";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);




                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                string stringData = JsonConvert.SerializeObject(objEmployee);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/Employee/" + objEmployee.EmpId, contentData);



                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objEmployee;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Employee> DeleteEmployee(string? empid)
        {
            try
            {
                string baseUrl = "https://localhost:7039";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);



                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                HttpResponseMessage response = await client.DeleteAsync
            ("/api/Employee/" + empid);


                string stringData = response.Content.
            ReadAsStringAsync().Result;
                Employee data = JsonConvert.DeserializeObject
            <Employee>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
