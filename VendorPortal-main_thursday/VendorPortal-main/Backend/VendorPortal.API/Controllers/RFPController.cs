using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorPortal.API.Data;
using VendorPortal.API.Models.Domain;
using VendorPortal.API.Models.DTO;

namespace VendorPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RFPController : ControllerBase
    {

        private readonly VendorPortalDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RFPController(VendorPortalDbContext dbContext, IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromForm] RFPDto rfpDto)
        {
            ValidateFileUpload(rfpDto.DocumentFile);

            if (ModelState.IsValid)
            {
                string docPath = await Upload(rfpDto.DocumentFile);

                var rfp = new RFP
                {
                    Title = rfpDto.Title,
                    Document = docPath,
                    ProjectId = rfpDto.ProjectId,
                    VendorCategoryId = rfpDto.VendorCategoryId,
                    EndDate = rfpDto.EndDate,
                };

                await dbContext.RFPs.AddAsync(rfp);
                await dbContext.SaveChangesAsync();
                return Ok(rfp);
            }
            else
            {
                return BadRequest("Document File Error");
            }
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var rfpResult = await dbContext.RFPs.Include("VendorCategory").Include("Project").FirstOrDefaultAsync(x => x.Id == id);

            if (rfpResult != null)
            {
                return Ok(rfpResult);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterVal)
        {

            var rfpsResult = dbContext.RFPs.Include("VendorCategory").Include("Project").AsQueryable();

            if (String.IsNullOrWhiteSpace(filterOn) == false && String.IsNullOrWhiteSpace(filterVal) == false)
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    rfpsResult = rfpsResult.Where(x => x.Title.ToLower().Contains(filterVal.ToLower())); 
                }

                if (filterOn.Equals("category", StringComparison.OrdinalIgnoreCase))
                {
                    rfpsResult = rfpsResult.Where(x => x.VendorCategory.Name.ToLower().Contains(filterVal.ToLower())); 
                }
            }

            if (rfpsResult != null)
            {
                return Ok(rfpsResult);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("VendorCategory/{id:Guid}")]
        public async Task<IActionResult> GetAllByVendorCategory([FromRoute] Guid id)
        {
            var rfpsResult = await dbContext.RFPs.Include("VendorCategory").Include("Project").Where(x => x.VendorCategoryId == id).ToListAsync();

            if (rfpsResult != null)
            {
                return Ok(rfpsResult);
            }

            return BadRequest("Something went wrong");
        }

        private async Task<string> Upload(IFormFile document)
        {
            var folder = Path.Combine(webHostEnvironment.ContentRootPath, "Files", "RFPDocuments");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var localFilePath = Path.Combine(folder, document.FileName);

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await document.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Files/RFPDocuments/{document.FileName}";

            var FilePath = urlFilePath;

            return FilePath;
        }

        private void ValidateFileUpload(IFormFile document)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };

            if (!allowedExtensions.Contains(Path.GetExtension(document.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (document.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }

    }
}