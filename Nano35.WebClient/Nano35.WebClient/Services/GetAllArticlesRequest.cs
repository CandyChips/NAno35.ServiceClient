using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllArticlesRequest : 
        RequestProvider<GetAllArticlesHttpQuery, GetAllArticlesSuccessHttpResponse>
    {
        public GetAllArticlesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllArticlesHttpQuery request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<GetAllArticlesSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/Articles?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllArticlesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}