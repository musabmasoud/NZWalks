using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            //Get from database -Domain Models
            var regionDomain = await dbContext.Regions.ToListAsync();
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var regions =dbContext.Regions.Find(id);
            //Get from database -Domain Models
            var regionsDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or convert to create DTO to Domain Model
            var regionDTO = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            //Use Domain model to create Region
           await dbContext.AddAsync(regionDTO);
           await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //Map Dto To Domain Model
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;
           await dbContext.SaveChangesAsync();
            //Convert Domain Model To DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(y => y.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();
            //Convert Domain Model To DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }
    }
}
