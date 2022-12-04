using Inventory.Domains.Models;
using Inventory.Responsive.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Inventory.Responsive.Services
{
    public class CategoriasServices : ICategoriasServices
    {

        public async Task<Categoria> AddCategoriasAsync(Categoria Categoria)
        {
            Categoria postCategoria = new Categoria();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Categoria), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7133/api/Categoria/categoria", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postCategoria = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return postCategoria;
        }

        public async Task<string> deleteCategoriasById(int categoriaId)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7133/api/Categoria/categoria?Codigo=" + categoriaId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }


        public async Task<Categoria> getCategoriaById(int categoriaId)
        {
            Categoria Categoria = new Categoria();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Categoria/categoria?Codigo=" + categoriaId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Categoria = JsonConvert.DeserializeObject<Categoria>(Response);
                    }
                }
            }
            return Categoria;
        }

        public async Task<IEnumerable<Categoria>> getCategoriasAsync()
        {
            List<Categoria> Categoria = new List<Categoria>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Categoria/categorias"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Categoria = JsonConvert.DeserializeObject<List<Categoria>>(Response);
                    }

                }
            }
            return Categoria;
        }

        public async Task<Categoria> updateCategoriasAsync(Categoria Categoria)
        {
            Categoria putCategoria = new Categoria();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Categoria), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7133/api/Categoria/categoria", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    putCategoria = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return putCategoria;
        }
    }
}
