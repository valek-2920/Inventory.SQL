using Inventory.Domains.Models;

namespace Inventory.Responsive.Services.IServices
{
    public interface IUsuariosServices
    {
        Task<IEnumerable<Usuarios>> getUsuariosAsync();
        Task<Usuarios> getUsuarioById(int usuarioId);
        Task<Usuarios> AddUsuariosAsync(Usuarios Usuarios);
        Task<string> deleteUsuariosById(int usuarioId);
        Task<Usuarios> updateUsuariosAsync(Usuarios Usuarios);
    }
}
