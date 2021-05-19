using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
    public class UpdateStorageItemRetailPriceRequest : 
        RequestProvider<UpdateStorageItemRetailPriceHttpBody, UpdateStorageItemRetailPriceSuccessHttpResponse>
    {
        public UpdateStorageItemRetailPriceRequest(IRequestManager requestManager, HttpClient httpClient, UpdateStorageItemRetailPriceHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateStorageItemRetailPriceSuccessHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.StorageServer}/StorageItems/RetailPrice",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateStorageItemRetailPriceSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}