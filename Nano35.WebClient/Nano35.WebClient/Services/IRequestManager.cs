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

    public class ClusterRequestManager :
        IRequestManager
    {
        private readonly HttpClient _httpClient;
        public ClusterRequestManager(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string IdentityServer => $"http://192.168.100.125:30001";
        public string InstanceServer => "http://192.168.100.125:30002";
        public string StorageServer => "http://192.168.100.125:30003";
        public string RepairOrdersServer => "http://192.168.100.125:30004";
        public string FileServer => "http://192.168.100.125:30005";
        public string CashboxServer => "http://192.168.100.125:30007";


        public async Task<bool> HealthCheck(string serverUrl)
        {
            var result = await _httpClient.GetAsync($"{serverUrl}/health");
            return result.IsSuccessStatusCode;
        }
    }
}
