
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VendorPortal.API.Data;
using VendorPortal.API.Models.Domain;
using VendorPortal.API.Models.DTO;

namespace VendorPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {

        private readonly VendorPortalDbContext dbContext;

        public ProjectController(VendorPortalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ProjectDto projectDto)
        {

            var newProject = new Project
            {
               
                Name = projectDto.Name,
                ProjectHeadId = projectDto.ProjectHeadId,
                ProjectStatus = projectDto.ProjectStatus,
                CreatedOn = DateTime.Now,
                Description = projectDto.Description,
            };

            await dbContext.Projects.AddAsync(newProject);

            await dbContext.SaveChangesAsync();
            return Ok(newProject);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var projectResult = await dbContext.Projects.Include("UserProfile").FirstOrDefaultAsync(x => x.Id == id);


            if (projectResult != null)
            {
                var project = new ProjectResponseDto
                {
                    Id = projectResult.Id,
                    Name = projectResult.Name,
                    ProjectStatus = projectResult.ProjectStatus,
                    CreatedOn = projectResult.CreatedOn,
                    Description = projectResult.Description,
                    ProjectHeadId = projectResult.UserProfile.Id,
                    ProjectHeadName = projectResult.UserProfile.Name,
                };

                return Ok(project);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery]string? filterVal)
        {
            var projectsResult = dbContext.Projects.Include("UserProfile").AsQueryable();
            

            if(String.IsNullOrWhiteSpace(filterOn)==false && String.IsNullOrWhiteSpace(filterOn) == false)
            {
                if (filterOn.Equals("projectHead",StringComparison.OrdinalIgnoreCase))
                {
                    projectsResult = projectsResult.Where(x=>x.UserProfile.Name.ToLower().Contains(filterVal.ToLower()));
                    
                }
                if(filterOn.Equals("projectStatus", StringComparison.OrdinalIgnoreCase))
                {
                    projectsResult = projectsResult.Where(x=>x.ProjectStatus.ToLower().Contains(filterVal.ToLower()));
                }
            }

            if (projectsResult != null)
            {
                List<ProjectResponseDto> result = new List<ProjectResponseDto>();
                foreach (var project in projectsResult)
                {
                    var newProject = new ProjectResponseDto
                    {
                        Id = project.Id,
                        Name = project.Name,
                        ProjectStatus = project.ProjectStatus,
                        CreatedOn = project.CreatedOn,
                        Description = project.Description,
                        ProjectHeadId = project.UserProfile.Id,
                        ProjectHeadName = project.UserProfile.Name,
                    };
                    result.Add(newProject);
                }
                
                return Ok(result);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("ProjectHead/{id:Guid}")]
        public async Task<IActionResult> GetAssignedProjectByUserId([FromRoute] string id)
        {

            var projectsResult = await  dbContext.Projects.Include(u => u.UserProfile).Where(x => x.ProjectHeadId == id).ToListAsync();


            if (projectsResult != null)
            {
                List<ProjectResponseDto> allProject = new List<ProjectResponseDto>();
                foreach(var project in projectsResult)
                {
                    var newProject = new ProjectResponseDto
                    {
                        Id = project.Id,
                        Name = project.Name,
                        ProjectStatus = project.ProjectStatus,
                        CreatedOn = project.CreatedOn,
                        Description = project.Description,
                        ProjectHeadId = project.UserProfile.Id,
                        ProjectHeadName = project.UserProfile.Name,
                    };
                    allProject.Add(newProject);
                }
                

                return Ok(allProject);
            }

            return BadRequest("Something went wrong");
        }

    }
}