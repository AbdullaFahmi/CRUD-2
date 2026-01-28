using CRUD_2.Data;
using CRUD_2.Models.DTO;
using CRUD_2.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public RegionController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        // GET: api/region
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

        // GET: api/region/{id}
        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
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

        // POST: api/region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDto requestDto)
        {
            var regionDomainModel = new Region
            {
                
                Code = requestDto.Code,
                Name = requestDto.Name,
                RegionImageUrl = requestDto.RegionImageUrl
            };

            dbcontext.Regions.Add(regionDomainModel);
            dbcontext.SaveChanges();

            var responseDto = new AddRegionDto
            {
              
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = regionDomainModel.Id },
                responseDto
            );
        }
    }
}
