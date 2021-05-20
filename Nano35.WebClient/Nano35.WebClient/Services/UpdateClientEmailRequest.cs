using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
    public class UpdateClientEmailRequest : 
        RequestProvider<UpdateClientsEmailHttpBody, UpdateClientsEmailSuccessHttpResponse>
    {
        public UpdateClientEmailRequest(IRequestManager requestManager, HttpClient httpClient, UpdateClientsEmailHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateClientsEmailSuccessHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.StorageServer}/Clients/{Request.ClientId}/Email",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateClientsEmailSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}