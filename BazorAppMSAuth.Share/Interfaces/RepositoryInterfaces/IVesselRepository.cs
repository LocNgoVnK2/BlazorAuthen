using BazorAppMSAuth.Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazorAppMSAuth.Share.Interfaces.RepositoryInterfaces
{
    public interface IVesselRepository
    {
        Task<IEnumerable<Vessel>> GetAllAsync();
        Task<Vessel> GetByIdAsync(int id);
        Task AddAsync(Vessel vessel);
        Task UpdateAsync(Vessel vessel);
        Task DeleteAsync(int id);
    }
}
