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
        public ClusterRequestManager(HttpClient httpClient) => _httpClient = httpClient;
        public string IdentityServer => "https://nano35.ru/api/identity";
        public string InstanceServer => "https://nano35.ru/api/instance";
        public string StorageServer => "https://nano35.ru/api/storage";
        public string RepairOrdersServer => "https://nano35.ru/api/repairorders";
        public string FileServer => "https://nano35.ru/api/storage";
        public string CashboxServer => "https://nano35.ru/api/Cashbox";
        public async Task<bool> HealthCheck(string serverUrl) => (await _httpClient.GetAsync($"{serverUrl}/health")).IsSuccessStatusCode;
    }
}
