using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllArticlesBrandsRequest : 
        RequestProvider<GetAllArticlesBrandsHttpQuery, GetAllArticleModelsSuccessHttpResponse>
    {
        public GetAllArticlesBrandsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllArticlesBrandsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllArticleModelsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.LocalStorageServer}/ArticleBrands?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllArticleModelsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}