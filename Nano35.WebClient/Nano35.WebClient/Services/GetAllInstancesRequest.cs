using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllInstancesRequest :
        RequestProvider<GetAllInstancesHttpQuery, GetAllInstancesSuccessHttpResponse>
    {
        public GetAllInstancesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllInstancesHttpQuery request) :
            base(requestManager, httpClient, request)
        {

        }

        public override async Task<GetAllInstancesSuccessHttpResponse> Send()
        {
            var response =
                await HttpClient.GetAsync(
                    $"{RequestManager.InstanceServer}/Instances/Current");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllInstancesSuccessHttpResponse>());
            }

            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}