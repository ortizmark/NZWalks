﻿using Microsoft.EntityFrameworkCore;
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
    }
}
