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
        string LocalIdentityServer { get; }
        string LocalInstanceServer { get; }
        string LocalStorageServer { get; }
        string LocalRepairOrdersServer { get; }
        string LocalFileServer { get; }
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
        public string IdentityServer => $"http://192.168.100.210:30001";
        public string InstanceServer => "http://192.168.100.210:30002";
        public string StorageServer => "http://192.168.100.210:30003";
        public string RepairOrdersServer => "http://192.168.100.210:30004";
        public string FileServer => "http://192.168.100.210:30005";
        public string LocalIdentityServer => "http://localhost:5001";
        public string LocalInstanceServer => "http://localhost:5002";
        public string LocalStorageServer => "http://localhost:5003";
        public string LocalRepairOrdersServer => "http://localhost:5004";
        public string LocalFileServer => "http://localhost:5005";
        
        
        public async Task<bool> HealthCheck(string serverUrl)
        {
            var result = await _httpClient.GetAsync($"{serverUrl}/health");
            return result.IsSuccessStatusCode;
        }
    }

    //public class LocalRequestManager :
    //    IRequestManager
    //{
    //    private readonly HttpClient _httpClient;
    //    public LocalRequestManager(
    //        HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }
    //    public string IdentityServer => "http://localhost:5001";
    //    public string InstanceServer => "http://localhost:5002";
    //    public string StorageServer => "http://localhost:5003";
    //    public string RepairOrdersServer => "http://localhost:5004";
    //    public string FileServer => "http://localhost:5005";
    //    public async Task<bool> HealthCheck(string serverUrl)
    //    {
    //        var result = await _httpClient.GetAsync($"{serverUrl}/health");
    //        return result.IsSuccessStatusCode;
    //    }
    //}
}
