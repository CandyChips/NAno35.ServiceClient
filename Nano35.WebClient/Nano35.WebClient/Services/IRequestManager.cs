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
        private readonly HttpClient _httpClient;
        public ClusterRequestManager(HttpClient httpClient) => 
            _httpClient = httpClient;
        public string IdentityServer => $"https://192.168.100.125/identity";
        public string InstanceServer => "https://192.168.100.125/instance";
        public string StorageServer => "https://192.168.100.125/storage";
        public string RepairOrdersServer => "https://192.168.100.125/repairorders";
        public string FileServer => "https://192.168.100.125:30005";
        public string CashboxServer => "https://192.168.100.125/Cashbox";
        public async Task<bool> HealthCheck(string serverUrl) => 
            (await _httpClient.GetAsync($"{serverUrl}/health")).IsSuccessStatusCode;
    }
}
