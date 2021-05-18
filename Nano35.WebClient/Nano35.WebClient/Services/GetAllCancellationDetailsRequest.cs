using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllCancellationDetails : 
        RequestProvider<GetAllCancellationDetailsHttpQuery, GetAllCancellationDetailsSuccessHttpResponse>
    {
        public GetAllCancellationDetails(IRequestManager requestManager, HttpClient httpClient, GetAllCancellationDetailsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllCancellationDetailsSuccessHttpResponse> Send()
        {
            var url = $"{RequestManager.StorageServer}/Cancellations/{Request.CancellationId}/Details";
            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllCancellationDetailsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}