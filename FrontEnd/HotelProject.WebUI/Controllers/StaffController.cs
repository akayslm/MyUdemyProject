using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class StaffController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //bir istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:5271/api/Staff"); //bu adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //başarılı bir durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veri jsondata değişkenine atanır
                var values = JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData); //gelen json tipindeki veriyi deserialize ederek tabloda gösterilebiliecek şekle dönüştürdük
                return View(values);    
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "applcation/json");
            var responseMessage = await client.PostAsync("http://localhost:5271/api/Staff", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5271/api/Staff/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
