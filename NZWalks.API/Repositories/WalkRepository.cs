using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
//using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext context;

        public WalkRepository(NZWalksDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Walk>> GetAll()
        {
            return await context.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalk(Guid id)
        {
            var walk = await context.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);

            return walk;
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {
            var currWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (currWalk == null)
                return null;

            currWalk.Length = walk.Length;
            currWalk.Name = walk.Name;
            currWalk.WalkDifficultyId = walk.WalkDifficultyId;
            currWalk.RegionId = walk.RegionId;

            await context.SaveChangesAsync();
            return currWalk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var walk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
                return null;
            context.Walks.Remove(walk);
            await context.SaveChangesAsync();
            return walk;
        }
    }
}
