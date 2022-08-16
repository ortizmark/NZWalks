using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
        Task<Region> GetRegion(Guid id);
        Task<Region> AddRegion(Region region);
        Task<Region> DeleteRegion(Guid id);
        Task<Region> UpdateRegion(Guid id, Region region);

    }
}
