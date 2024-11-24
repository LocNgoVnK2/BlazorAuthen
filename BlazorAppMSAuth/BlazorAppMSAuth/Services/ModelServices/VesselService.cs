using BazorAppMSAuth.Share.Interfaces.RepositoryInterfaces;
using BazorAppMSAuth.Share.Interfaces.ServicesInterfaces;
using BazorAppMSAuth.Share.Models;

namespace BlazorAppMSAuth.Services.ModelServices
{
    public class VesselService : IVesselService
    {
        private readonly IVesselRepository _vesselRepository;

        public VesselService(IVesselRepository vesselRepository)
        {
            _vesselRepository = vesselRepository;
        }

        public async Task<IEnumerable<Vessel>> GetAllVesselsAsync()
        {
            return await _vesselRepository.GetAllAsync();
        }

        public async Task<Vessel> GetVesselByIdAsync(int id)
        {
            return await _vesselRepository.GetByIdAsync(id);
        }

        public async Task AddVesselAsync(Vessel vessel)
        {
            await _vesselRepository.AddAsync(vessel);
        }

        public async Task UpdateVesselAsync(Vessel vessel)
        {
            await _vesselRepository.UpdateAsync(vessel);
        }
        public async Task DeleteVesselAsync(int id)
        {
            await _vesselRepository.DeleteAsync(id);
        }
    }

}
