using System.Net.Http;
using System.Threading.Tasks;

namespace Nano35.WebClient.Services
{
    
    public abstract class RequestProvider<TReq, TSRes>
    {
        protected readonly HttpClient HttpClient;
        protected readonly IRequestManager RequestManager;
        protected readonly TReq Request;

        protected RequestProvider(IRequestManager requestManager, HttpClient httpClient, TReq request)
        {
            RequestManager = requestManager;
            HttpClient = httpClient;
            Request = request;
        }

        public abstract Task<TSRes> Send();
    }
}