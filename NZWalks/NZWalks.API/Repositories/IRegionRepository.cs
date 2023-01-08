using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //IEnumerable<Region> GetAll();
        //Making Repository Asynchronous
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
