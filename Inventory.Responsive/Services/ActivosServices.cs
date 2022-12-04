using Inventory.Domains.Models;
using Inventory.Responsive.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Inventory.Responsive.Services
{
    public class ActivosServices : IActivosServices
    {
        public async Task<Activos> AddActivosAsync(Activos Activos)
        {
            Activos postActivos = new Activos();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Activos), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7133/api/Activos/activo", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postActivos = JsonConvert.DeserializeObject<Activos>(apiResponse);
                }
            }
            return postActivos;
        }

        public async Task<string> deleteActivosById(int activoId)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7133/api/Activos/activo?Codigo=" + activoId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }

        public async Task<Activos> getActivoById(int activoId)
        {
            Activos Activos = new Activos();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Activos/activo?Codigo=" + activoId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Activos = JsonConvert.DeserializeObject<Activos>(Response);
                    }
                }
            }
            return Activos;
        }

        public async Task<IEnumerable<Activos>> getActivosAsync()
        {            List<Activos> Activos = new List<Activos>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Activos/activos"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Activos = JsonConvert.DeserializeObject<List<Activos>>(Response);
                    }

                }
            }
            return Activos;
        }


        public async Task<Activos> updateActivosAsync(Activos Activos)
        {
            Activos putActivos = new Activos();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Activos), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7133/api/Activos/activo", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    putActivos = JsonConvert.DeserializeObject<Activos>(apiResponse);
                }
            }
            return putActivos;
        }
    }
}
