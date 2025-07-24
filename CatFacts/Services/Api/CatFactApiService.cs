using CatFacts.Models;
using CatFacts.Services.Logger;

namespace CatFacts.Services.Api;

public interface ICatFactApiService
{
    public Task<CatFact?> Get();
}

public class CatFactApiService : ICatFactApiService
{
    private readonly HttpClient _httpClient;
    private readonly IInformationLogger _logger;

    public CatFactApiService(IHttpClientFactory _httpClientFactory, IInformationLogger logger)
    {
        _httpClient = _httpClientFactory.CreateClient("BackendApi");
        _logger = logger;
    }

    public async Task<CatFact?> Get()
    {
        var response = await _httpClient.GetAsync("/fact");

        response.EnsureSuccessStatusCode();

        CatFact? catFact = await response.Content.ReadFromJsonAsync<CatFact>();

        if (catFact is not null)
        {
            _logger.Log(catFact.ToString());
        }

        return catFact;
    }
}
