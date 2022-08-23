using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await walkRepository.GetAll();
            if (walks == null)
                return NotFound();
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walksDTO);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walk = await walkRepository.GetWalk(id);
            if (walk == null)
                return NotFound();
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Models.DTO.AddWalkRequest addWalk)
        {
            var walk = mapper.Map<Models.Domain.Walk>(addWalk);

            walk = await walkRepository.AddWalk(walk);
            if (walk == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTO.Region>(walk);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequest UpdatedWalk)
        {
            var walk = mapper.Map<Models.Domain.Walk>(UpdatedWalk);
            walk = await walkRepository.UpdateWalk(id, walk);
            if (walk == null)
                return NotFound();

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walk = await walkRepository.DeleteWalk(id);
            if (walk == null)
                return NotFound();

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }




    }
}
