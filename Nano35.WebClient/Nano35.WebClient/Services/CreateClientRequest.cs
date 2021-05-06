using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class CreateClientRequest : 
        RequestProvider<CreateClientHttpBody, CreateClientSuccessHttpResponse>
    {
        public CreateClientRequest(IRequestManager requestManager, HttpClient httpClient, CreateClientHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateClientSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.InstanceServer}/Clients", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateClientSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}