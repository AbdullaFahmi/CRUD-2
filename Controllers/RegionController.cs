using CRUD_2.Data;
using CRUD_2.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CRUD_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly AppDbContext dbcontext;
        private object regionsDto;

        public RegionController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbcontext.Regions.ToList();
            var regionsDto = regions.Select(region => new RegionDto
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            }).ToList();

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbcontext.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
