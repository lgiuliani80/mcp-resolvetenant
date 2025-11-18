/// <summary>
/// File: TenantTool.cs
/// Author: [Your Name]
/// Date Created: 2025-05-08
/// Description: Provides tools for interacting with Entra ID (Azure Active Directory) and other utilities.
/// </summary>
[McpServerToolType]
public static class TenantTool
{
    [McpServerTool, Description("Gets the Entra ID (Azure Active Directory) tenant id (guid) from a domain name")]
    public static async Task<string> GetTenantId([Description("DNS domain name associated to the tenant")] string domainName)
    {
        var tenantId = await EntraIdHelper.GetTenantId(domainName);
        return tenantId;
    }
}
