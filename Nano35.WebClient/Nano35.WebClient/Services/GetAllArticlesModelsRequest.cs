using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllArticlesModelsRequest : 
        RequestProvider<GetAllArticleModelsHttpQuery, GetAllArticleModelsSuccessHttpResponse>
    {
        public GetAllArticlesModelsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllArticleModelsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllArticleModelsSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.StorageServer}/ArticleModels?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllArticleModelsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}