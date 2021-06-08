using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public abstract class RequestProviderr<TReq, TSRes>
    {
        protected readonly HttpClient HttpClient;
        protected readonly RequestManager RequestManager;
        protected readonly TReq Request;

        protected RequestProviderr(RequestManager requestManager, HttpClient httpClient, TReq request)
        {
            RequestManager = requestManager;
            HttpClient = httpClient;
            Request = request;
        }

        public abstract Task<TSRes> Send();
    }

    public class GetRequestProvider<TReq, TSRes> : RequestProviderr<TReq, TSRes>
    {
        public GetRequestProvider(HttpClient httpClient, TReq request) : base(null, httpClient, request) { }
        private string _addr;
        
        public async Task<TSRes> Ask(string addr)
        {
            _addr = addr;
            return await Send();
        }
        
        public override async Task<TSRes> Send()
        {
            var response = await HttpClient.GetAsync(_addr);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TSRes>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}