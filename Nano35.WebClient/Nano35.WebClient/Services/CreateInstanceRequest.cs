using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class CreateInstanceRequest : 
        RequestProvider<CreateInstanceHttpBody, CreateInstanceSuccessHttpResponse>
    {
        public CreateInstanceRequest(IRequestManager requestManager, HttpClient httpClient, CreateInstanceHttpBody request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<CreateInstanceSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Instances/CreateInstance");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateInstanceSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}