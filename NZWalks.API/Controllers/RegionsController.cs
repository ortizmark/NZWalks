using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize (Roles = "reader")]
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
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.GetRegion(id);
            if (region == null)
                return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegion(Models.DTO.AddRegionRequest addRegion)
        {
            //if (!ValidateAddRegion(addRegion))
            //    return BadRequest(ModelState);

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
        [Authorize(Roles = "writer")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest UpdatedRegion)
        {
            //if (!ValidateUpdateRegion(id, UpdatedRegion))
            //    return BadRequest(ModelState);

            var region = mapper.Map<Models.Domain.Region>(UpdatedRegion);
            region = await regionRepository.UpdateRegion(id, region);
            if (region == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        #region Private method

        private Boolean ValidateAddRegion(Models.DTO.AddRegionRequest addRegion)
        {
            if (addRegion == null)
            {
                ModelState.AddModelError(nameof(addRegion), $"Add Region is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegion.Code))
            {
                ModelState.AddModelError(nameof(addRegion.Code), $"{nameof(addRegion.Code)} cannot be null or white space");
            }
            if (string.IsNullOrWhiteSpace(addRegion.Name))
            {
                ModelState.AddModelError(nameof(addRegion.Name), $"{nameof(addRegion.Name)} cannot be null or white space");
            }
            if (addRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Area), $"{nameof(addRegion.Area)} must be greater than zero");
            }
            if (addRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Lat), $"{nameof(addRegion.Lat)} must be greater than zero");
            }
            if (addRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Long), $"{nameof(addRegion.Long)} must be greater than zero");
            }
            if (addRegion.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegion.Population), $"{nameof(addRegion.Population)} must be greater than zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private Boolean ValidateUpdateRegion(Guid id, UpdateRegionRequest UpdatedRegion)
        {
            if (UpdatedRegion == null)
            {
                ModelState.AddModelError(nameof(UpdatedRegion), $"Updated Region is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(UpdatedRegion.Code))
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Code), $"{nameof(UpdatedRegion.Code)} cannot be null or white space");
            }
            if (string.IsNullOrWhiteSpace(UpdatedRegion.Name))
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Name), $"{nameof(UpdatedRegion.Name)} cannot be null or white space");
            }
            if (UpdatedRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Area), $"{nameof(UpdatedRegion.Area)} must be greater than zero");
            }
            if (UpdatedRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Lat), $"{nameof(UpdatedRegion.Lat)} must be greater than zero");
            }
            if (UpdatedRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Long), $"{nameof(UpdatedRegion.Long)} must be greater than zero");
            }
            if (UpdatedRegion.Population < 0)
            {
                ModelState.AddModelError(nameof(UpdatedRegion.Population), $"{nameof(UpdatedRegion.Population)} must be greater than zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}
