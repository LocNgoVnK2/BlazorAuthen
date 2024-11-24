using BazorAppMSAuth.Share.Interfaces.RepositoryInterfaces;
using BazorAppMSAuth.Share.Models;
using BlazorAppMSAuth.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppMSAuth.Repository
{
    public class VesselRepository : IVesselRepository
    {
        private readonly ApplicationDbContext _context;

        public VesselRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vessel>> GetAllAsync()
        {
            return await _context.Vessels.ToListAsync();
        }

        public async Task<Vessel> GetByIdAsync(int id)
        {
            return await _context.Vessels.FindAsync(id);
        }

        public async Task AddAsync(Vessel vessel)
        {
            // Xử lý thông tin audit trước khi thêm
            vessel.CreatedDate = DateTime.UtcNow;
            vessel.CreatedBy = "System"; // Có thể thay bằng người dùng hiện tại
            _context.Vessels.Add(vessel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vessel vessel)
        {
            var existingVessel = await _context.Vessels.FindAsync(vessel.Id);
            if (existingVessel != null)
            {
                existingVessel.Name = vessel.Name;
                existingVessel.ModifiedDate = DateTime.UtcNow;
                existingVessel.ModifiedBy = "System"; // Cập nhật với người dùng thực hiện hành động
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var vessel = await _context.Vessels.FindAsync(id);
            if (vessel != null)
            {
                _context.Vessels.Remove(vessel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
