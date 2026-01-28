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
    public class DifficultyController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public DifficultyController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var difficulties = dbcontext.Difficulties.ToList();

            var difficultyDtos = difficulties.Select(difficulty => new DIfficultyDto
            {
                Name = difficulty.Name
            }).ToList();

            return Ok(difficultyDtos);
        }
        [HttpPost]
        public IActionResult AddDifficulty([FromBody] DIfficultyDto requestDto)
        {
            var difficultyDomainModel = new Difficulty
            {
                Name = requestDto.Name
            };

            dbcontext.Difficulties.Add(difficultyDomainModel);
            dbcontext.SaveChanges();

            var responseDto = new DIfficultyDto 
            {
                
                Name = requestDto.Name 
            }; 

            return Created();
        }

    }
}
