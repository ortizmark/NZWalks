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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, 
            IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            if (!(await ValidateAdd(addWalk)))
                return BadRequest(ModelState);

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
            if (!(await ValidateUpdateWalk(id, UpdatedWalk)))
                return BadRequest(ModelState);

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

        #region Private method

        private async Task<Boolean> ValidateAdd(Models.DTO.AddWalkRequest addWalk)
        {
            //if (addWalk == null)
            //{
            //    ModelState.AddModelError(nameof(addWalk), $"Add Walk is required");
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(addWalk.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalk.Name), $"{nameof(addWalk.Name)} cannot be null or white space");
            //}
            //if (addWalk.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalk.Length), $"{nameof(addWalk.Length)} must be greater than zero");
            //}

            var region = await regionRepository.GetRegion(addWalk.RegionId);
            if (region == null)
                ModelState.AddModelError(nameof(addWalk.RegionId), $"{nameof(addWalk.RegionId)} is invalid");

            var wd = await walkDifficultyRepository.GetWalkDifficulty(addWalk.WalkDifficultyId);
            if (wd == null)
                ModelState.AddModelError(nameof(addWalk.WalkDifficultyId), $"{nameof(addWalk.WalkDifficultyId)} is invalid");

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<Boolean> ValidateUpdateWalk(Guid id, UpdateWalkRequest UpdatedWalk)
        {
            //if (UpdatedWalk == null)
            //{
            //    ModelState.AddModelError(nameof(UpdatedWalk), $"Update Walk is required");
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(UpdatedWalk.Name))
            //{
            //    ModelState.AddModelError(nameof(UpdatedWalk.Name), $"{nameof(UpdatedWalk.Name)} cannot be null or white space");
            //}
            //if (UpdatedWalk.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(UpdatedWalk.Length), $"{nameof(UpdatedWalk.Length)} must be greater than zero");
            //}

            var region = await regionRepository.GetRegion(UpdatedWalk.RegionId);
            if (region == null)
                ModelState.AddModelError(nameof(UpdatedWalk.RegionId), $"{nameof(UpdatedWalk.RegionId)} is invalid");

            var wd = await walkDifficultyRepository.GetWalkDifficulty(UpdatedWalk.WalkDifficultyId);
            if (wd == null)
                ModelState.AddModelError(nameof(UpdatedWalk.WalkDifficultyId), $"{nameof(UpdatedWalk.WalkDifficultyId)} is invalid");

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
