using Inventory.Domains.Models;
using Inventory.Responsive.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Inventory.Responsive.Services
{
    public class DonacionesServices : IDonacionesServices
    {

        public async Task<Donaciones> AddDonacionesAsync(Donaciones Donaciones)
        {
            Donaciones postDonaciones = new Donaciones();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Donaciones), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7133/api/Donaciones/donacion", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postDonaciones = JsonConvert.DeserializeObject<Donaciones>(apiResponse);
                }
            }
            return postDonaciones;
        }

        public async Task<string> deleteDonacionesById(int donacionId)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7133/api/Donaciones/donacion?Codigo=" + donacionId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }

        public async Task<Donaciones> getDonacionById(int donacionId)
        {
            Donaciones Donaciones = new Donaciones();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Donaciones/donacion?Codigo=" + donacionId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Donaciones = JsonConvert.DeserializeObject<Donaciones>(Response);
                    }
                }
            }
            return Donaciones;
        }

        public async Task<IEnumerable<Donaciones>> getDonacionesAsync()
        {
            List<Donaciones> Donaciones = new List<Donaciones>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7133/api/Donaciones/donaciones"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        Donaciones = JsonConvert.DeserializeObject<List<Donaciones>>(Response);
                    }

                }
            }
            return Donaciones;
        }

        public async Task<Donaciones> updateDonacionesAsync(Donaciones Donaciones)
        {
            Donaciones putDonaciones = new Donaciones();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Donaciones), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7133/api/Donaciones/donacion", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    putDonaciones = JsonConvert.DeserializeObject<Donaciones>(apiResponse);
                }
            }
            return putDonaciones;
        }
    }
}
