using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetArticleByIdRequest : 
        RequestProvider<GetArticleByIdHttpQuery, GetArticleByIdSuccessHttpResponse>
    {
        public GetArticleByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetArticleByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetArticleByIdSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/Articles/{Request.Id}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetArticleByIdSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}