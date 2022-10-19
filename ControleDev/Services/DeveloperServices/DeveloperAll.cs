using Newtonsoft.Json;
using ControleDev.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDev.Services
{
    public class DeveloperAll
    {
        public async Task<Developer[]> ReturnAll()
        {
            HttpClient httpClient = new HttpClient();

            try
            {
                var returnAll = await httpClient.GetAsync("https://61a170e06c3b400017e69d00.mockapi.io/DevTest/Dev");
                var jsonString = await returnAll.Content.ReadAsStringAsync();
            
                Developer[] jsonObjectAll = JsonConvert.DeserializeObject<Developer[]>(jsonString);

                return jsonObjectAll;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Developer> ReturnDev(int id)
        {
            HttpClient httpClient = new HttpClient();

            try
            {
                var returnAll = await httpClient.GetAsync($"https://61a170e06c3b400017e69d00.mockapi.io/DevTest/Dev/{id}");
                var jsonString = await returnAll.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Developer>(jsonString);
            }
            catch
            {
                return null;
            }

        }

        public async void Insert(Developer obj)
        {
            obj.CreatedAt = DateTime.Now;

            var url = "https://61a170e06c3b400017e69d00.mockapi.io/DevTest/Dev";
            HttpClient httpClient = new HttpClient();
            using var response = await httpClient.PostAsJsonAsync(url, obj);
        }

        public async void Update(int id, Developer obj)
        {
            var url = "https://61a170e06c3b400017e69d00.mockapi.io/DevTest/Dev";

            using (var httpClient = new HttpClient())
            {
               await httpClient.PutAsJsonAsync($"{url}/{id}", obj); 
            }
        }


        public async void Remove(int id)
        {
            var url = "https://61a170e06c3b400017e69d00.mockapi.io/DevTest/Dev";

            using (var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync($"{url}/{id}");
            }
        }

    }
}
