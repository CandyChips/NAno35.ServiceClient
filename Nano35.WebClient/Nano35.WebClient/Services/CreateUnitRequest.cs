using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class CreateUnitRequest : 
        RequestProvider<CreateUnitHttpBody, CreateUnitSuccessHttpResponse>
    {
        public CreateUnitRequest(IRequestManager requestManager, HttpClient httpClient, CreateUnitHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateUnitSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalInstanceServer}/Units/CreateUnit", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateUnitSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}