using Inventory.Domains.Models;

namespace Inventory.Responsive.Services.IServices
{
    public interface ICategoriasServices
    {
        Task<IEnumerable<Categoria>> getCategoriasAsync();
        Task<Categoria> getCategoriaById(int categoriaId);
        Task<Categoria> AddCategoriasAsync(Categoria Categoria);
        Task<string> deleteCategoriasById(int categoriaId);
        Task<Categoria> updateCategoriasAsync(Categoria Categoria);
    }
}
