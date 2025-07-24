namespace CatFacts.Helpers;

public static class SafeApiCall
{
    public static async Task<T?> ExecuteAsync<T>(Func<Task<T?>> apiCall, Action<Exception>? onError = null)
    {
        try
        {
            return await apiCall();
        }
        catch (Exception ex)
        {
            onError?.Invoke(ex);
            return default;
        }
    }
}
