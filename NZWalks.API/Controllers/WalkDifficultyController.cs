using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulty()
        {
            var wd = await walkDifficultyRepository.GetAll();
            if (wd == null)
                return NotFound();
            var wdDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(wd);
            return Ok(wdDTO);
        }

        [HttpGet("{id:guid}")]
        //[Route("{id:guid}")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var wd = await walkDifficultyRepository.GetWalkDifficulty(id);
            if (wd == null)
                return NotFound();
            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty(Models.DTO.AddWalkDifficultyRequest addWD)
        {
            var wd = mapper.Map<Models.Domain.WalkDifficulty>(addWD);
            //var region = new Models.Domain.Region()
            //{
            //     Area = addRegion.Area,
            //     Code = addRegion.Code,    
            //     Lat = addRegion.Lat,
            //     Long = addRegion.Long,
            //     Name = addRegion.Name,
            //     Population = addRegion.Population
            //};

            wd = await walkDifficultyRepository.AddWalkDifficulty(wd);
            if (wd == null)
                return NotFound();

            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var wd = await walkDifficultyRepository.DeleteWalkDifficulty(id);
            if (wd == null)
                return NotFound();

            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest UpdatedWD)
        {
            var wd = mapper.Map<Models.Domain.WalkDifficulty>(UpdatedWD);
            wd = await walkDifficultyRepository.UpdateWalkDifficulty(id, wd);
            if (wd == null)
                return NotFound();

            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }



    }
}
