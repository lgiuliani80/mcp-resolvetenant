var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.SingleLine = true;
    options.TimestampFormat = "[HH:mm:ss.fff] ";
    options.UseUtcTimestamp = true;
});

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();