public static class EntraIdHelper
{
    private static readonly HttpClient _httpClient3 = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false });

    public static async Task<string> GetTenantId(string domainName)
    {
        var url = $"https://login.microsoftonline.com/{domainName}/v2.0/.well-known/openid-configuration";
        var response = await _httpClient3.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new Exception($"Tenant not found for domain: {domainName}");
        }
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);
        var issuer = json.RootElement.GetProperty("issuer").GetString();
        var segments = issuer?.Split('/');
        if (segments == null || segments.Length < 4)
        {
            throw new Exception($"Invalid issuer format: {issuer}");
        }

        return segments[3];
    }
}