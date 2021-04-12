using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateCancellationRequest : 
        RequestProvider<CreateCancellationHttpBody, CreateCancellationSuccessHttpResponse>
    {
        public CreateCancellationRequest(IRequestManager requestManager, HttpClient httpClient, CreateCancellationHttpBody request) : 
            base(requestManager, httpClient, request)
        { }

        public override async Task<CreateCancellationSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalStorageServer}/Warehouse/CreateCancellation", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateCancellationSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}