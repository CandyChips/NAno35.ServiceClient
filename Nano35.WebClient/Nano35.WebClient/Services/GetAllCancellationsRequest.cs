using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllCancellationsRequest : 
        RequestProvider<GetAllCancellationsHttpQuery, GetAllCancellationsSuccessHttpResponse>
    {
        public GetAllCancellationsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllCancellationsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetAllCancellationsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/Cancellations?InstanceId={Request.InstanceId}&UnitId={Request.UnitId}");
            if (response.IsSuccessStatusCode) 
            {
                return (await response.Content.ReadFromJsonAsync<GetAllCancellationsSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}