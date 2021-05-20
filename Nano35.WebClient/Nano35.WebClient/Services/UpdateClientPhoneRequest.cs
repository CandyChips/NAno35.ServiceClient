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
    public class UpdateClientPhoneRequest : 
        RequestProvider<UpdateClientsPhoneHttpBody, UpdateClientsPhoneSuccessHttpResponse>
    {
        public UpdateClientPhoneRequest(IRequestManager requestManager, HttpClient httpClient, UpdateClientsPhoneHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateClientsPhoneSuccessHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.StorageServer}/Clients/{Request.ClientId}/Phone",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateClientsPhoneSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}