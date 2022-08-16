using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext context;

        public RegionRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegion(Guid id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == id );
            return region;
        }

        public async Task<Region> AddRegion(Region region)    
        {
            region.Id = Guid.NewGuid();
            await context.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
                return null;
            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
            var currRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (currRegion == null)
                return null;

            currRegion.Area = region.Area;
            currRegion.Code = region.Code;
            currRegion.Lat = region.Lat;
            currRegion.Long = region.Long;
            currRegion.Name = region.Name;
            currRegion.Population = region.Population;

            await context.SaveChangesAsync();
            return currRegion;
        }
    }
}
