using CRUD_2.Data;
using CRUD_2.Models.DTO;
using CRUD_2.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CRUD_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        public readonly AppDbContext dbcontext;

        public WalkController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var walks = dbcontext.Walks
                                 .Include(walk => walk.Difficulty)
                                 .Include(walk => walk.Region)
                                 .ToList();

            var walkDtos = walks.Select(walk => new WalkDto
            {
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                DifficultyId = walk.DifficultyId,
                RegionId = walk.RegionId,
                DifficultyName = walk.Difficulty.Name,
                RegionName = walk.Region.Name
            }).ToList();

            return Ok(walkDtos);
        }

        [HttpPost]

        public IActionResult CreateWalk([FromBody] WalkDto requestDto)
        {
            var walkDomainModel = new Walk()
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                LengthInKm = requestDto.LengthInKm,
                WalkImageUrl = requestDto.WalkImageUrl,
                DifficultyId = requestDto.DifficultyId,
                RegionId = requestDto.RegionId,

            };
            dbcontext.Walks.Add(walkDomainModel);
            dbcontext.SaveChanges();

            var responseDto = new WalkDto
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                LengthInKm = requestDto.LengthInKm,
                WalkImageUrl = requestDto.WalkImageUrl,
                DifficultyId = requestDto.DifficultyId,
                RegionId = requestDto.RegionId,
                DifficultyName = requestDto.DifficultyName,
                RegionName = requestDto.RegionName
            };

            return Created();

        }
    }
}


