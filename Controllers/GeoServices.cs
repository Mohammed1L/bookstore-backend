using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class GeoService
{
    private readonly HttpClient _httpClient;

    public GeoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(double lat, double lng)> GeocodeAddressAsync(string address)
    {
        var url = $"https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?f=json&SingleLine={Uri.EscapeDataString(address)}&outFields=Match_addr,Addr_type";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to geocode");

        var content = await response.Content.ReadAsStringAsync();
        using JsonDocument doc = JsonDocument.Parse(content);
        var candidates = doc.RootElement.GetProperty("candidates");

        if (candidates.GetArrayLength() == 0)
            throw new Exception("No candidates found");

        var location = candidates[0].GetProperty("location");
        double x = location.GetProperty("x").GetDouble();
        double y = location.GetProperty("y").GetDouble();

        return (y, x); 
    }
}