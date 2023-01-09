using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {

            var regions = await regionRepository.GetAllAsync();
            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            // {
            //     //Assigning Domain Models To DTO Models
            //     var regionDTO = new Models.DTO.Region() 
            //     { 
            //     Id= region.Id,
            //     Name = region.Name,
            //     Area= region.Area,
            //     Code=region.Code,  
            //     Lat=region.Lat,
            //     Long=region.Long,
            //     Population=region.Population
            //     };
            //     regionsDTO.Add(regionDTO);
            // });

            //return Ok(regionsDTO);

            //With the help of AutoMapper
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };

            //Pass details to Repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get Region from the Database
            var region = await regionRepository.DeleteAsync(id);
            //If null NotFound
            if (region == null)
            {
                return NotFound();
            }
            //Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //return Ok response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            //Update Region using Repository
            region = await regionRepository.UpdateAsync(id, region);
            //If Null, then NotFound
            if (region == null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //return OK Response
            return Ok(regionDTO);
        }
    }
}