using Inventory.Domains.Models;

namespace Inventory.Responsive.Services.IServices
{
    public interface IActivosServices
    {
        Task<IEnumerable<Activos>> getActivosAsync();
        Task<Activos> getActivoById(int activoId);
        Task<Activos> AddActivosAsync(Activos Activos);
        Task<string> deleteActivosById(int activoId);
        Task<Activos> updateActivosAsync(Activos Activos);
    }
}
