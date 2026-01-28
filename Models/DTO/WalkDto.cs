using CRUD_2.Models.Entities;

namespace CRUD_2.Models.DTO
{
    public class WalkDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }
       
        public string? DifficultyName { get; set; }
        public string? RegionName { get; set; }
    }
}

    