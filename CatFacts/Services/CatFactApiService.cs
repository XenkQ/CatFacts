using CatFacts.Models;

namespace CatFacts.Services;

public interface ICatFactApiService
{
    public Task<CatFact> Get();
}

public class CatFactApiService : ICatFactApiService
{
    private readonly HttpClient _httpClient;

    public CatFactApiService(IHttpClientFactory _httpClientFactory)
    {
        _httpClient = _httpClientFactory.CreateClient("BackendApi");
    }

    public async Task<CatFact> Get()
    {
        var response = await _httpClient.GetAsync("/fact");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CatFact>();
    }
}
