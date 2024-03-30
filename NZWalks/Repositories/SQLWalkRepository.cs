using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNamber = 1, int pageSize = 1000)
        {
            //filtering
            var walks = dbContext.Walks.Include("Diffculty").Include("Region").AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks=walks.Where(x=>x.Name.Contains(filterQuery)); 
                }
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks=isAscending? walks.OrderBy(x=>x.LengthInKm):walks.OrderByDescending(x=>x.LengthInKm);
                }
            }
            //Pagination
            var skipResults = (pageNamber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbContext.Walks.Include("Diffculty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Diffculty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existtingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existtingWalk == null)
            {
                return null;
            }
            existtingWalk.Name = walk.Name;
            existtingWalk.Description = walk.Description;
            existtingWalk.LengthInKm = walk.LengthInKm;
            existtingWalk.WalkImageUrl = walk.WalkImageUrl;
            existtingWalk.DiffcultyId = walk.DiffcultyId;
            existtingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existtingWalk;
        }
    }
}
