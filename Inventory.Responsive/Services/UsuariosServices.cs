using Inventory.Domains.Models;
using Inventory.Responsive.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Inventory.Responsive.Services
{
    public class UsuariosServices : IUsuariosServices
    {

        public async Task<Usuarios> AddUsuariosAsync(Usuarios Usuarios)
        {
            Usuarios postUsuarios = new Usuarios();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Usuarios), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7133/api/Usuarios/usuario", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postUsuarios = JsonConvert.DeserializeObject<Usuarios>(apiResponse);
                }
            }
            return postUsuarios;
        }

        public async Task<string> deleteUsuariosById(int usuarioId)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7133/api/Usuarios/usuario?Cedula=" + usuarioId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }

        public async Task<Usuarios> getUsuarioById(int usuarioId)
        {
            Usuarios Usuarios = new Usuarios();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Usuarios/usuario?Cedula=" + usuarioId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Usuarios = JsonConvert.DeserializeObject<Usuarios>(Response);
                    }
                }
            }
            return Usuarios;
        }

        public async Task<IEnumerable<Usuarios>> getUsuariosAsync()
        {
            List<Usuarios> Usuarios = new List<Usuarios>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Usuarios/usuarios"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(Response);
                    }

                }
            }
            return Usuarios;
        }

        public async Task<Usuarios> updateUsuariosAsync(Usuarios Usuarios)
        {
            Usuarios putUsuarios = new Usuarios();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Usuarios), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7133/api/Usuarios/usuario", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    putUsuarios = JsonConvert.DeserializeObject<Usuarios>(apiResponse);
                }
            }
            return putUsuarios;
        }

    }
}
