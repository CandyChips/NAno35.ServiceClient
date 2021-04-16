using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateComingRequest : 
        RequestProvider<CreateComingHttpBody, CreateComingSuccessHttpResponse>
    {
        public CreateComingRequest(IRequestManager requestManager, HttpClient httpClient, CreateComingHttpBody request) : 
            base(requestManager, httpClient, request)
        { }

        public override async Task<CreateComingSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalStorageServer}/Warehouse/CreateComing", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateComingSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}