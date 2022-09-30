using EmployeeManagement.WebAPI.Authentication;
using EmployeeManagement.Entities; 
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeManagement.MVC;
using EmployeeManagement.MVC.Models;

namespace EmployeeManagement.MVC.Controllers { 

    public class AuthenticateMVCController : Controller

{

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("UserName,Password")] LoginModel obj)
    {
        var returnedloginmodel = await AddLoginModel(obj);

        var handler = new JwtSecurityTokenHandler();
        var token2 = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
        var token = handler.ReadJwtToken(token2);


        if (token2 != null)

        {

            var role = token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
                if (role.Value == "Admin" || role.Value == "admin" || role.Value == "ADMIN")
                {
                    return RedirectToAction("HomeAdmin", "Home");
                }


                if (role.Value == "Employee" || role.Value == "employee" || role.Value == "EMPLOYEE")
                {
                    return RedirectToAction("HomeEmployee", "Home");
                }
          

            
                     
        }

        return View(obj);
    }


    public async Task<LoginModel> AddLoginModel(LoginModel objLoginModel)
    {

        string baseUrl = "https://localhost:7039";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        var contentType = new MediaTypeWithQualityHeaderValue    ("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);

        string stringData = JsonConvert.SerializeObject(objLoginModel);
        var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("/api/Authentication/login", contentData);


        if (response.IsSuccessStatusCode)
        {

            string stringJWT = response.Content.
            ReadAsStringAsync().Result;
            JWT jwt = JsonConvert.DeserializeObject  <JWT>(stringJWT);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "token", jwt.Token);

        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ViewBag.Message = "Unauthorized!";
        }
        return objLoginModel;

    }


    public async Task<IActionResult> Register()
    {
        List<IdentityRole> roles = await GetRoles();
        
        var sortedRoles = roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        
        ViewData["Role"] = sortedRoles;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register([Bind("UserName,Email,Password,Role")] RegisterModel obj)
    {
        var returnedregistermodel = await AddRegisterModel(obj);
        if (returnedregistermodel != null)
        {
            return RedirectToAction("HomeAdmin", "Home");
        }
        return View(obj);
        ;
    }


    public async Task<RegisterModel> AddRegisterModel(RegisterModel registermodel)
    {


        string baseUrl = "https://localhost:7039";
        HttpClient client = new HttpClient();
        
        client.BaseAddress = new Uri(baseUrl);
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        string stringData = JsonConvert.SerializeObject(registermodel);
        var contentData = new StringContent(stringData,
    System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("api/Authentication/registeradmin", contentData);

        if (response.IsSuccessStatusCode)
        {
            return registermodel;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ViewBag.Message = "Unauthorized!";
        }
        return registermodel;
    }
    private object RegisterModelExists(string email)
    {
        throw new NotImplementedException();
    }
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Remove("token");
        ViewBag.Message = "User logged out successfully!";
        return View("Index");
    }


    public async Task<List<IdentityRole>> GetRoles()
    {
        string baseUrl = "https://localhost:7039";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);

        HttpResponseMessage response = await client.GetAsync("/api/Authentication");
        string stringData = response.Content.ReadAsStringAsync().Result;
        List<IdentityRole> data = JsonConvert.DeserializeObject<List<IdentityRole>>(stringData);

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
}
}





