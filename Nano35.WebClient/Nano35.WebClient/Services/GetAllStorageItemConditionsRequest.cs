using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllStorageItemConditionsRequest : 
        RequestProvider<GetAllStorageItemConditionsHttpQuery, GetAllStorageItemConditionsSuccessHttpResponse>
    {
        public GetAllStorageItemConditionsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllStorageItemConditionsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllStorageItemConditionsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5003/StorageItems/GetAllStorageItemConditions");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllStorageItemConditionsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}