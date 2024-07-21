using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApi.API.CustomActionFilters;
using MyFirstApi.API.Data;
using MyFirstApi.API.Models.Domain;
using MyFirstApi.API.Models.DTO;
using MyFirstApi.API.Repositories;

namespace MyFirstApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly MyFirstApiDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //ctor skrót do controllera
        public RegionsController(MyFirstApiDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database = Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map domain models to dtos
            var regionsDto=mapper.Map<List<RegionDto>>(regionsDomain);
            //return dtos

            return Ok(regionsDto);
        }

        //Get single Region (Get Region By ID)
        [HttpGet]
        [Authorize(Roles = "Reader")]
        [Route("{id:Guid}")]
        //jak bym chciał znaleźć po code to miałbym: [Route("{code:string}")]
        public async Task <IActionResult> GetById([FromRoute] Guid id)
        {
            //Get region Domain MOdel From Database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            //var region = dbContext.Regions.FirstOrDefault(x => x.Code == code);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST To Create New Region
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task <IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel =await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update Region
        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

           regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null) 
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        //Delete Region
        [HttpDelete]
        [Authorize(Roles = "Writer,Reader")]
        [Route("{id:Guid}")]
        public async Task <IActionResult> Delete([FromRoute] Guid id) 
        {
            var regionDomainModel= await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
