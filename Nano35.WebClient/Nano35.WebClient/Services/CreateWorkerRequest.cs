using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class CreateWorkerRequest : 
        RequestProvider<CreateWorkerHttpBody, CreateWorkerSuccessHttpResponse>
    {
        public CreateWorkerRequest(IRequestManager requestManager, HttpClient httpClient, CreateWorkerHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateWorkerSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.InstanceServer}/Workers/CreateWorker", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateWorkerSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}