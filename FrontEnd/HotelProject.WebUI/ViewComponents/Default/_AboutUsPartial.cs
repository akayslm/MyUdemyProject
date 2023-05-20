﻿using HotelProject.WebUI.Dtos.AboutDto;
using HotelProject.WebUI.Dtos.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _AboutUsPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AboutUsPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); //bir istemci oluşturduk
            var responseMessage = await client.GetAsync("http://localhost:5224/api/About"); //bu adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //başarılı bir durum kodu dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veri jsondata değişkenine atanır
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData); //gelen json tipindeki veriyi deserialize ederek tabloda gösterilebiliecek şekle dönüştürdük
                return View(values);
            }
            return View();
        }
    }
}
