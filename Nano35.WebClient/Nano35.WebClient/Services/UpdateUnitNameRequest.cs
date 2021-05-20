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
    public class UpdateUnitNameRequest : 
        RequestProvider<UpdateUnitsNameHttpBody, UpdateUnitsNameSuccessHttpResponse>
    {
        public UpdateUnitNameRequest(IRequestManager requestManager, HttpClient httpClient, UpdateUnitsNameHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<UpdateUnitsNameSuccessHttpResponse> Send()
        {
            HttpContent req = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var response = await HttpClient.PatchAsync($"{RequestManager.StorageServer}/Units/{Request.UnitId}/Name",req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateUnitsNameSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}