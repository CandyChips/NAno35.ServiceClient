using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetInstanceByIdRequest : 
        RequestProvider<GetInstanceByIdHttpQuery, GetInstanceByIdSuccessHttpResponse>
    {
        public GetInstanceByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetInstanceByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetInstanceByIdSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Instances/GetInstanceById?Id={Request.Id}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetInstanceByIdSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}