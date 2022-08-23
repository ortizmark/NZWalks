using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAll();
        Task<WalkDifficulty> GetWalkDifficulty(Guid id);
        Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty wd);
        Task<WalkDifficulty> DeleteWalkDifficulty(Guid id);
        Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty wd);
    }
}
