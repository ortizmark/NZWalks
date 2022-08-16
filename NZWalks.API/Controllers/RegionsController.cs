using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAll(); 
            if (regions == null)
                return NotFound();
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet("{id:guid}")]
        //[Route("{id:guid}")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.GetRegion(id);
            if (region == null)
                return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(Models.DTO.AddRegionRequest addRegion)
        {
            var region = mapper.Map<Models.Domain.Region>(addRegion);
            //var region = new Models.Domain.Region()
            //{
            //     Area = addRegion.Area,
            //     Code = addRegion.Code,    
            //     Lat = addRegion.Lat,
            //     Long = addRegion.Long,
            //     Name = addRegion.Name,
            //     Population = addRegion.Population
            //};

            region = await regionRepository.AddRegion(region);
            if (region == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionRepository.DeleteRegion(id);
            if (region == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest UpdatedRegion)
        {
            var region = mapper.Map<Models.Domain.Region>(UpdatedRegion);
            region = await regionRepository.UpdateRegion(id, region);
            if (region == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

    }
}
