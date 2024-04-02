using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get from database -Domain Models
            var regionDomain = await regionRepository.GetAllAsync();
            //Return DTOs
            return Ok(mapper.Map<List<RegionDTO>>(regionDomain));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var regions =dbContext.Regions.Find(id);
            //Get from database -Domain Models
            var regionsDomain = await regionRepository.GetByIdAsync(id);
            if(regionsDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models To DTOs
            return Ok(mapper.Map<RegionDTO>(regionsDomain));
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
                //Map or Convert DTO to Domain Model
                var regjonDomainModel = mapper.Map<Region>(addRegionRequestDTO);

                //Use Domain model to create Region
                await regionRepository.CreateAsync(regjonDomainModel);
                //Map Domain Model back to DTO
                var regionDTO = mapper.Map<RegionDTO>(regjonDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
           
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
                // Map DTO To Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //Convert Domain Model To DTO
                var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
                return Ok(regionDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model To DTO
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }
    }
}
