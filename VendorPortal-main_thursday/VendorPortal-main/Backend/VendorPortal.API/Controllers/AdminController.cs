using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendorPortal.API.Models.DTO;
using VendorPortal.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace VendorPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<UserProfile> userManager;

        public AdminController(UserManager<UserProfile> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var adminsResult = await userManager.GetUsersInRoleAsync("Admin");
            if (adminsResult != null)
            {
                List<AdminResponseDto> allAdmins = new List<AdminResponseDto>();

                foreach (var singleAdmin in adminsResult)
                {
                    var admin = new AdminResponseDto
                    {
                        Id = singleAdmin.Id,
                        Name = singleAdmin.Name,
                        Email = singleAdmin.Email,
                        PhoneNumber = singleAdmin.PhoneNumber,
                    };

                    allAdmins.Add(admin);
                }

                return Ok(allAdmins);
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AdminDto adminDto)
        {

            var adminProfile = new UserProfile
            {
                UserName = adminDto.Email,
                Name = adminDto.Name,
                PhoneNumber = adminDto.PhoneNumber,
                Email = adminDto.Email,
               
            };

            var adminResult = await userManager.CreateAsync(adminProfile, adminDto.Password);

            if (adminResult.Succeeded)
            {
                List<string> roles = new List<string>();
                roles.Add("Admin");
                var adminRoleResult = await userManager.AddToRolesAsync(adminProfile, roles);
                if (adminRoleResult.Succeeded)
                {
                    return Ok("Admin was registered! Please login.");
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var adminResult = await userManager.FindByIdAsync(id);
            if (adminResult != null)
            {
                var admin = new AdminResponseDto
                {
                    Id = adminResult.Id,
                    Name = adminResult.Name,
                    PhoneNumber = adminResult.PhoneNumber,
                    Email = adminResult.Email,
                    
                };
                return Ok(admin);
            }

            return BadRequest();
        }

    }
}
