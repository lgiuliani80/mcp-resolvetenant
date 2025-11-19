# ResolveTenant (mcp-resolvetenant)

A .NET 9.0 application for hosting and running Model Context Protocol (MCP) servers and tools. This project provides a simple way to instantiate an MCP server using standard IO as the transport layer, and automatically loads tool assemblies for MCP.

## Features

- Console hosting of MCP servers using .NET 9.0 (`Microsoft.Extensions.Hosting`)
- Logging with detailed timestamps, scopes, and customizable console output
- Pluggable transport via standard input/output (`WithStdioServerTransport`)
- Tools discovery and loading from assemblies (`WithToolsFromAssembly`)
- Designed to work with the ModelContextProtocol

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- (Optional) ModelContextProtocol tools and plugins

### Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/lgiuliani80/mcp-resolvetenant.git
   cd mcp-resolvetenant
   ```

2. Restore dependencies:
   ```sh
   dotnet restore
   ```

3. Build the application:
   ```sh
   dotnet build
   ```

4. Run the server:
   ```sh
   dotnet run
   ```

### Project Structure

- `Program.cs`: Main entry point, configures and launches the MCP server with console logging and tool/transport integration.
- `ResolveTenant.csproj`: Project configuration, targeting `net9.0`, referencing `Microsoft.Extensions.Hosting` and `ModelContextProtocol`.
- `Tools/`: Place your custom MCP tool implementations here (details may vary per project).
- `Utils/`: Auxiliary scripts or utilities related to MCP workflows.
- Other configuration files: `.gitignore`, `.gitattributes`, and solution/project files.

## Example Usage

After starting, the server listens over STDIO and loads tool plugins for handling MCP requests.

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddSimpleConsole(options => {
    options.IncludeScopes = true;
    options.SingleLine = true;
    options.TimestampFormat = "[HH:mm:ss.fff] ";
    options.UseUtcTimestamp = true;
});
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
```

## Integration: Add to mcp.json

To add `ResolveTenant` as a tool/server in your MCP ecosystem, introduce an entry in your `mcp.json` configuration referencing the executable path and the stdio transport, for example:

```json
{
  "getTenantId": {
    "command": "/path/to/ResolveTenant(.exe)",
    "type": "stdio"
  }
}
```

> **Note:** Adjust the "command" path as needed to match your installation and platform (Windows: .exe, Linux/macOS: ELF binary if applicable). The "type" must be set to "stdio" for MCP-based tools communicating over standard input/output.

## Dependencies

- [Microsoft.Extensions.Hosting (v9.0.4)](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)
- [ModelContextProtocol (v0.1.0-preview.11)](https://www.nuget.org/packages/ModelContextProtocol)

## License

[MIT](LICENSE) (If not present, specify here)

## Contributing

Feel free to open issues or pull requests for enhancements, bug fixes, or tool integrations.

---

For details or advanced configuration, see the source code or contact [lgiuliani80](https://github.com/lgiuliani80).