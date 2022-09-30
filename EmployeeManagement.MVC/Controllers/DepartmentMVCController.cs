using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
namespace EmployeeManagement.MVC.Controllers
{
   
    public class DepartmentMVCController : Controller
    {
        public DepartmentMVCController()
        {

        }

        public async Task<IActionResult> Index()
        {
            try
            {

                List<Department> objDepartments = await GetDepartments();

                return View(objDepartments);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptId,DeptName")] Department Department)
        {
            try
            {


                await AddDepartment(Department);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Department);
        }

       //

        public IActionResult Edit()
        {
            return View();
        }
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int deptId, [Bind("DeptId,DeptName")] Department Department)
        {
            if (deptId != Department.DeptId)
            {
                return NotFound();
            }


            try
            {
                await UpdateDepartment(Department);

            }
            catch (Exception)
            {
                if (DepartmentExists(Department.DeptId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(Department);
        }
      

        // GET: DepartmentsMVC/Delete/5

          public IActionResult Delete()
          {
              return View();
          }

          [HttpDelete]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Delete(int? deptId)
          {
              if (deptId == null)
              {
                  return NotFound();
              }

              try
              {


                  Department objDepartment = await GetDepartment(deptId);

                  if (objDepartment == null)
                  {
                      return NotFound();
                  }

                  return View(objDepartment);
              }
              catch
              {
                  throw;
              }
          }

          // POST: DepartmentsMVC/Delete/5
          [HttpDelete]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Delete(int deptId)
          {
              try
              {
                  await DeleteDepartment(deptId);
                  return RedirectToAction(nameof(Index));
              }
              catch (Exception)
              {
                  throw;
              }
          }

        private async Task<Department> DepartmentExists(int deptId)
        {

            Department objDepartment = await GetDepartment(deptId);
            return objDepartment;

        }

        public async Task<List<Department>> GetDepartments()
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




                HttpResponseMessage response = await client.GetAsync("/api/Department");

                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Department> data = JsonConvert.DeserializeObject<List<Department>>(stringData);

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

        public async Task<Department> GetDepartment(int? deptId)
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


                HttpResponseMessage response = await client.GetAsync("/api/Department/" + deptId);


                string stringData = response.Content.ReadAsStringAsync().Result;
                Department data = JsonConvert.DeserializeObject     <Department>(stringData);

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

        public async Task<Department> AddDepartment(Department objDepartment)
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



                string stringData = JsonConvert.SerializeObject(objDepartment);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Department/", contentData);



                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objDepartment;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<Department> UpdateDepartment(Department objDepartment)
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



                string stringData = JsonConvert.SerializeObject(objDepartment);
                var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/Department/" + objDepartment.DeptId, contentData);


               
                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objDepartment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Department> DeleteDepartment(int? deptId)
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



                HttpResponseMessage response = await client.DeleteAsync ("/api/Department/" + deptId);


                string stringData = response.Content.ReadAsStringAsync().Result;
                Department data = JsonConvert.DeserializeObject<Department>(stringData);

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
