using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateSelleRequest : 
        RequestProvider<CreateSelleHttpBody, CreateSelleSuccessHttpResponse>
    {
        public CreateSelleRequest(IRequestManager requestManager, HttpClient httpClient, CreateSelleHttpBody request) : 
            base(requestManager, httpClient, request)
        { }

        public override async Task<CreateSelleSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.StorageServer}/Warehouse/CreateSelle", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateSelleSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}