using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.WebAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<EmployeeManagement.WebAPI.Authentication.ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        //public AutheticationController(UserManager<EmployeeManagement.WebAPI.Authentication.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        public AuthenticationController(UserManager<EmployeeManagement.WebAPI.Authentication.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("registeradmin")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  //[FromBody]
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            //-----------
            if (!await roleManager.RoleExistsAsync(UserRoles.ADMIN))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN));
            if (!await roleManager.RoleExistsAsync(UserRoles.EMPLOYEE))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.EMPLOYEE));

            if (await roleManager.RoleExistsAsync(UserRoles.ADMIN))
            {
                await userManager.AddToRoleAsync(user, UserRoles.ADMIN);
            }
            //=++
            if (await roleManager.RoleExistsAsync(UserRoles.EMPLOYEE))
            {
                await userManager.AddToRoleAsync(user, UserRoles.EMPLOYEE);
            }

            //-------------


            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
