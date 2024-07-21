using Microsoft.EntityFrameworkCore;
using MyFirstApi.API.Data;
using MyFirstApi.API.Models.Domain;

namespace MyFirstApi.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly MyFirstApiDbContext dbContext;
        public SQLRegionRepository(MyFirstApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exisitngRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (exisitngRegion == null) { 
                return null;
            }

            dbContext.Regions.Remove(exisitngRegion);

            await dbContext.SaveChangesAsync();
            return exisitngRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
