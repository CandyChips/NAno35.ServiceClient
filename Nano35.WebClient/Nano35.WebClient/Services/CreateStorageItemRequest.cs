using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateStorageItemRequest : 
        RequestProvider<CreateStorageItemHttpBody, CreateStorageItemSuccessHttpResponse>
    {
        public CreateStorageItemRequest(IRequestManager requestManager, HttpClient httpClient, CreateStorageItemHttpBody request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<CreateStorageItemSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalStorageServer}/StorageItems/StorageItem", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateStorageItemSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}