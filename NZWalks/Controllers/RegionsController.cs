using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get from database -Domain Models
            var regionDomain = dbContext.Regions.ToList();
            //Map Domain Models To DTOs
            var regionDTO = new List<RegionDTO>();
            foreach (var regionDomains in regionDomain)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regionDomains.Id,
                    Name = regionDomains.Name,
                    Code = regionDomains.Code,
                    RegionImageUrl = regionDomains.RegionImageUrl
                });

            }

            //Return DTOs
            return Ok(regionDTO);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var regions =dbContext.Regions.Find(id);
            //Get from database -Domain Models
            var regionsDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionsDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models To DTOs
            var regionDTO = new RegionDTO
            {
                Id = regionsDomain.Id,
                Name = regionsDomain.Name,
                Code = regionsDomain.Code,
                RegionImageUrl = regionsDomain.RegionImageUrl
            };

            //Return DTOs
            return Ok(regionDTO);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or convert to create DTO to Domain Model
            var regionDTO = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            //Use Domain model to create Region
            dbContext.Add(regionDTO);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }
    }
}
