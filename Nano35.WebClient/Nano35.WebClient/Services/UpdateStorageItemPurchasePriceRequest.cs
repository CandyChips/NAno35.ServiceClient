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
    public class UpdateStorageItemPurchasePriceRequest : 
        RequestProvider<UpdateStorageItemPurchasePriceHttpBody, UpdateStorageItemPurchasePriceSuccessHttpResponse>
    {
        public UpdateStorageItemPurchasePriceRequest(IRequestManager requestManager, HttpClient httpClient, UpdateStorageItemPurchasePriceHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateStorageItemPurchasePriceSuccessHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.StorageServer}/StorageItems/PurchasePrice",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateStorageItemPurchasePriceSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}