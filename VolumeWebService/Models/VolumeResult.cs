using System.Text.Json.Serialization;

namespace VolumeWebService.Models
{
    public enum Type
    {
        Cone,
        Cylinder
    }
    
    public class VolumeResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("type")]
        public Type Type { get; set; }
        
        [JsonPropertyName("height")]
        public decimal Height { get; set; }
        
        [JsonPropertyName("radius")]
        public decimal Radius { get; set; }
        
        [JsonPropertyName("volume")]
        public double Volume { get; set; }
    }
}