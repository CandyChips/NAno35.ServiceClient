using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class CreateWorkRequest : 
        RequestProvider<CreateWorkHttpBody, CreateWorkHttpResponse>
    {
        public CreateWorkRequest(IRequestManager requestManager, HttpClient httpClient, CreateWorkHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateWorkHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.RepairOrdersServer}/Works", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateWorkHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}