using HotelProject.WebUI.Dtos.ServiceDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //bir istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:5271/api/Service"); //bu adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //başarılı bir durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veri jsondata değişkenine atanır
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData); //gelen json tipindeki veriyi deserialize ederek tabloda gösterilebiliecek şekle dönüştürdük
                return View(values);
            }
            return View();
        }
    }
}
