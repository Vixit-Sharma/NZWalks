using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.Regions.AddAsync(region);
            nZWalksDbContext.SaveChanges();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            //Delete the Region
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            //return nZWalksDbContext.Regions.ToList();
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            //nZWalksDbContext.Update(existingRegion);
            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
