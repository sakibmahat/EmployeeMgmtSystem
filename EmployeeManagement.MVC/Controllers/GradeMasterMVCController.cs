using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace EmployeeManagement.MVC.Controllers

{
    public class GradeMasterMVCController : Controller
    {
        public GradeMasterMVCController()
        {

        }

        public async Task<IActionResult> Index()
        {
            try
            {

                List<GradeMaster> objGradeMasters = await GetGradeMasters();

                return View(objGradeMasters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(string? gradeCode)
        {
            if (gradeCode == null)
            {
                return NotFound();
            }
            try
            {


                var GradeMaster = await GetGradeMaster(gradeCode);

                if (GradeMaster == null)
                {
                    return NotFound();
                }

                return View(GradeMaster);
            }
            catch
            {
               // if (gradeCode==0)
               if(gradeCode == null)
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
        public async Task<IActionResult> Create([Bind("GradeCode,Designation,MaxSalary,MinSalary")] GradeMaster GradeMaster)
        {
            try
            {


                await AddGradeMaster(GradeMaster);
            }
            catch (Exception)
            {
                throw;
            }
            return View(GradeMaster);
        }

        // GET: GradeMastersMVC/Edit/5
        public async Task<IActionResult> Edit(string? gradeCode)
        {
            if (gradeCode == null)
            {
                return NotFound();
            }
            try
            {


                //var GradeMaster = await _context.GradeMasters.FindAsync(id);
                var GradeMaster = await GetGradeMaster(gradeCode);
                if (GradeMaster == null)
                {
                    return NotFound();
                }

                return View(GradeMaster);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string gradeCode, [Bind("GradeCode,Designation,MaxSalary,MinSalary")] GradeMaster GradeMaster)
        {
            if (gradeCode != GradeMaster.GradeCode)
            {
                return NotFound();
            }


            try
            {
                await UpdateGradeMaster(GradeMaster);

            }
            catch (Exception)
            {
                if (GradeMasterExists(GradeMaster.GradeCode) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(GradeMaster);

        }

        // GET: GradeMastersMVC/Delete/5

        public async Task<IActionResult> Delete(string? gradeCode)
        {
            if (gradeCode == null)
            {
                return NotFound();
            }

            try
            {


                GradeMaster objGradeMaster = await GetGradeMaster(gradeCode);

                if (objGradeMaster == null)
                {
                    return NotFound();
                }

                return View(objGradeMaster);
            }
            catch
            {
                throw;
            }
        }

        // POST: GradeMastesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string? gradeCode)
      /*  public async Task<IActionResult> Delete(string? gradecode)
        {
            try
            {
                await DeleteGradeMaster(gradecode);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }*/

        private async Task<GradeMaster> GradeMasterExists(string gradeCode)
        {
            //return _context.GradeMasters.Any(e => e.Id == id);
            GradeMaster objGradeMaster = await GetGradeMaster(gradeCode);
            return objGradeMaster;

        }

        public async Task<List<GradeMaster>> GetGradeMasters()
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




                HttpResponseMessage response = await client.GetAsync
            ("/api/GradeMaster");

                string stringData = response.Content.
            ReadAsStringAsync().Result;
                List<GradeMaster> data = JsonConvert.DeserializeObject
            <List<GradeMaster>>(stringData);

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

        public async Task<GradeMaster> GetGradeMaster(string? gradeCode)
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


                HttpResponseMessage response = await client.GetAsync
            ("/api/GradeMaster/" + gradeCode);


                string stringData = response.Content.
            ReadAsStringAsync().Result;
                GradeMaster data = JsonConvert.DeserializeObject
            <GradeMaster>(stringData);

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

        public async Task<GradeMaster> AddGradeMaster(GradeMaster objGradeMaster)
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



                string stringData = JsonConvert.SerializeObject(objGradeMaster);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/GradeMaster/", contentData);



                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objGradeMaster;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<GradeMaster> UpdateGradeMaster(GradeMaster objGradeMaster)
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



                string stringData = JsonConvert.SerializeObject(objGradeMaster);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/GradeMaster/" + objGradeMaster.GradeCode, contentData);


                

                if (response.IsSuccessStatusCode)
                {

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return objGradeMaster;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GradeMaster> DeleteGradeMaster(string? gradeCode)
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
            ("/api/GradeMaster/" + gradeCode);


                string stringData = response.Content.
            ReadAsStringAsync().Result;
                GradeMaster data = JsonConvert.DeserializeObject
            <GradeMaster>(stringData);

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
