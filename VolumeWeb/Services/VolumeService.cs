using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VolumeWeb.Models;
using System.Text.Json;

namespace VolumeWeb.Services
{
    public interface IVolumeService
    {
        Task<List<VolumeResult>> GetSavedVolumeResults();
        Task<VolumeResult> GetVolumeResult(decimal height, decimal radius, Type type);
        Task<VolumeResult> SaveAndGetVolumeResult(decimal height, decimal radius, Type type);
    }
    
    public class VolumeService : IVolumeService
    {
        private readonly HttpClient _httpClient;

        public VolumeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<VolumeResult>> GetSavedVolumeResults()
        {
            var response =
                await _httpClient.GetStringAsync("calculate");
            
            return JsonSerializer.Deserialize<List<VolumeResult>>(response);
        }
        
        public async Task<VolumeResult> GetVolumeResult(decimal height, decimal radius, Type type)
        {
            var response =
                await _httpClient.GetStringAsync(
                    $"calculate/{(type == Type.Cone ? "cone" : "cylinder")}?height={height}&radius={radius}");
            
            return JsonSerializer.Deserialize<VolumeResult>(response);
        }
        
        public async Task<VolumeResult> SaveAndGetVolumeResult(decimal height, decimal radius, Type type)
        {
            VolumeResult volumeResult = new VolumeResult {Height = height, Radius = radius, Type = type};
            var content = new StringContent(JsonSerializer.Serialize(volumeResult), Encoding.UTF8, "application/json");
            
            var response =
                await _httpClient.PostAsync(
                    $"calculate/{(type == Type.Cone ? "cone" : "cylinder")}?height={height}&radius={radius}", null);
            
            return JsonSerializer.Deserialize<VolumeResult>(await response.Content.ReadAsStringAsync());
        }
    }
}