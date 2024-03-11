using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VendorPortal.API.Mail;
using VendorPortal.API.Models.Domain;
using VendorPortal.API.Models.DTO;

namespace VendorPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectHeadController : ControllerBase
    {

        private readonly UserManager<UserProfile> userManager;
        private readonly EmailService emailService;


        public ProjectHeadController(UserManager<UserProfile> userManager, EmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] ProjectHeadDto projectHeadDto)
        {

            var newProjectHead = new UserProfile
            {
               
                Name = projectHeadDto.Name,
                PhoneNumber = projectHeadDto.PhoneNumber,
                Email = projectHeadDto.Email,
                UserName = projectHeadDto.UserName,
            };

            var projectHeadResult = await userManager.CreateAsync(newProjectHead, "Pass@123");

            if (projectHeadResult.Succeeded)
            {
                List<string> roles = new List<string>();
                roles.Add("ProjectHead");
                projectHeadResult = await userManager.AddToRolesAsync(newProjectHead, roles);

                if (projectHeadResult.Succeeded)
                {
                    SendWelcomeEmail(newProjectHead);
                    return Ok("ProjectHead was registered! Please login.");
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {

            var projectHeadResult = await userManager.FindByIdAsync(id);

            if (projectHeadResult != null)
            {
                var projectHead = new ProjectHeadResponseDto
                {
                    Id = projectHeadResult.Id,
                    Name = projectHeadResult.Name,
                    Email = projectHeadResult.Email,
                    PhoneNumber = projectHeadResult.PhoneNumber,

                };

                return Ok(projectHead);
            }


            return BadRequest("Something went wrong");
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ProjectHeadUpdateDto projectHeadUpdateDto)
        {

            var projectHeadResult = await userManager.FindByIdAsync(id);

            if (projectHeadResult != null)
            {
                if (projectHeadUpdateDto.NewPassword != "")
                {
                    var passResult = await userManager.ChangePasswordAsync(projectHeadResult, projectHeadUpdateDto.CurrentPassword, projectHeadUpdateDto.NewPassword);
                    if (!passResult.Succeeded)
                    {
                        return BadRequest(passResult.Errors);
                    }
                }
                projectHeadResult.Name = projectHeadUpdateDto.Name;
                projectHeadResult.PhoneNumber = projectHeadUpdateDto.PhoneNumber;

              
                await userManager.UpdateAsync(projectHeadResult);

                var projectHead = new ProjectHeadResponseDto
                {
                    Id = projectHeadResult.Id,
                    Name = projectHeadResult.Name,
                    Email = projectHeadResult.Email,
                    PhoneNumber = projectHeadResult.PhoneNumber,

                };

                return Ok(projectHead);
            }


            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll([FromQuery] string? nameVal)
        {
            var dbProjectHeadsResult = await userManager.GetUsersInRoleAsync("ProjectHead");
            var projectHeadsResult = dbProjectHeadsResult.AsQueryable();

            if (String.IsNullOrWhiteSpace(nameVal) == false)
            {
                projectHeadsResult = projectHeadsResult.Where(x => x.Name.ToLower().Contains(nameVal.ToLower()));
            }

            if (projectHeadsResult != null)
            {
                List<ProjectHeadResponseDto> allProjectHead = new List<ProjectHeadResponseDto>();
                foreach (var projectHead in projectHeadsResult)
                {
                    var newProjectHead = new ProjectHeadResponseDto
                    {
                        Id = projectHead.Id,
                        Name = projectHead.Name,
                        Email = projectHead.Email,
                        PhoneNumber = projectHead.PhoneNumber,
                    };
                    allProjectHead.Add(newProjectHead);
                }

                return Ok(allProjectHead);
            }
            return BadRequest("Something went wrong");
        }

        private void SendWelcomeEmail(UserProfile user)
        {
            string subject = $"Welcome to Our Application Project Head ID {user.Id}";
            string body = $"Dear {user.Name},\n\nWelcome to our application! Your username is: {user.UserName} and your password is: Pass@123\n\nBest regards,\nYour Application Team";

            emailService.SendEmail(user.Email, subject, body);
        }
    }
}