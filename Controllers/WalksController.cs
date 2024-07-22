using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.API.CustomActionFilters;
using MyFirstApi.API.Models.Domain;
using MyFirstApi.API.Models.DTO;
using MyFirstApi.API.Repositories;

namespace MyFirstApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper=mapper;
            this.walkRepository = walkRepository;
        }
        // CREATE walk
        // POST: /api.walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
                //Map Dto to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }

        // Get walks
        //With filter, sort and pagination
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //Create an exception
           // throw new Exception("This is a new exception");

            //Map Domain Model to Dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //Get walk by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null) 
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update walk by id
        //PUT
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

                // Map DTO to domain model
                var walkDomainModel= mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if(walkDomainModel == null)
                {
                   return NotFound();
                }

                //map domain model to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //delete walk by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel=await walkRepository.DeleteAsync(id);
            if(deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));

        }
    }
}
