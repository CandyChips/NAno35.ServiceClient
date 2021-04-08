using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class CreateWorkRequest : 
        RequestProvider<CreateWorkBody, CreateWorkSuccessResponse>
    {
        public CreateWorkRequest(IRequestManager requestManager, HttpClient httpClient, CreateWorkBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateWorkSuccessResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"http://localhost:5004/Work/CreateWork", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateWorkSuccessResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}