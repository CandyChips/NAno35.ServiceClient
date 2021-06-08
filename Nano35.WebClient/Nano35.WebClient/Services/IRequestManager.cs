using System.Net.Http;
using System.Threading.Tasks;

namespace Nano35.WebClient.Services
{
    public interface IRequestManager
    {
        string IdentityServer { get; }
        string InstanceServer { get; }
        string StorageServer { get; }
        string RepairOrdersServer { get; }
        string FileServer { get; }
        string CashboxServer { get; }
        Task<bool> HealthCheck(string serverUrl);
    }

    public class ClusterRequestManager : IRequestManager
    {
        private const string ProxyUri = "https://nano35.ru/api";
        private readonly HttpClient _httpClient;
        public ClusterRequestManager(HttpClient httpClient) => _httpClient = httpClient;
        public string IdentityServer => ProxyUri;
        public string InstanceServer => ProxyUri;
        public string StorageServer => ProxyUri;
        public string RepairOrdersServer => ProxyUri;
        public string CashboxServer => ProxyUri;
        public string FileServer => $"{ProxyUri}/storage";
        public async Task<bool> HealthCheck(string serverUrl) => (await _httpClient.GetAsync($"{serverUrl}/health")).IsSuccessStatusCode;
    }
}
