using BazorAppMSAuth.Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazorAppMSAuth.Share.Interfaces.ServicesInterfaces
{
    public interface IVesselService
    {

        Task<IEnumerable<Vessel>> GetAllVesselsAsync();
        Task<Vessel> GetVesselByIdAsync(int id);
        Task AddVesselAsync(Vessel vessel);
        Task UpdateVesselAsync(Vessel vessel);
        Task DeleteVesselAsync(int id);
    }
}
