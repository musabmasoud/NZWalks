﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MinLength(100)]
        public string Name { get; set; }
        [Required]
        [MinLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }
        [Required]
        public Guid DiffcultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
