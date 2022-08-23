using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext context;

        public WalkDifficultyRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty wd)
        {
            wd.Id = Guid.NewGuid();
            await context.WalkDifficulty.AddAsync(wd);
            await context.SaveChangesAsync();
            return wd;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficulty(Guid id)
        {
            var wd = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (wd == null)
                return null;
            context.WalkDifficulty.Remove(wd);
            await context.SaveChangesAsync();
            return wd;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
            return await context.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficulty(Guid id)
        {
            var wd = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            return wd;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty wd)
        {
            var currWD = await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (currWD == null)
                return null;

            currWD.Code = wd.Code;

            await context.SaveChangesAsync();
            return currWD;
        }
    }
}
