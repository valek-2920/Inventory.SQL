using Inventory.Domains.Models;

namespace Inventory.Responsive.Services.IServices
{
    public interface IDonacionesServices
    {
        Task<IEnumerable<Donaciones>> getDonacionesAsync();
        Task<Donaciones> getDonacionById(int donacionId);
        Task<Donaciones> AddDonacionesAsync(Donaciones Donaciones);
        Task<string> deleteDonacionesById(int donacionId);
        Task<Donaciones> updateDonacionesAsync(Donaciones Donaciones);
    }
}
