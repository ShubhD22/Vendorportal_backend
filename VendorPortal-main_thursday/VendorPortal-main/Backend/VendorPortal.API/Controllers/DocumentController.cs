using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorPortal.API.Data;
using VendorPortal.API.Models.Domain;
using VendorPortal.API.Models.DTO;

namespace VendorPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {

        private readonly VendorPortalDbContext dbContext;

        public DocumentController(VendorPortalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost]
        [Route("Add")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] DocumentDto documentDto)
        {

            var document = new Document
            {
                Name = documentDto.Name,
                Description = documentDto.Description
            };

            await dbContext.Documents.AddAsync(document);
            await dbContext.SaveChangesAsync();
            return Ok(document);
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var documentResult = await dbContext.Documents.ToListAsync();

            if (documentResult != null) {
                
                return Ok(documentResult);
            }

            return BadRequest("Something went wrong");
                
        }

    }
}