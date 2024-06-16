namespace BlogAgro.API.Filters
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
