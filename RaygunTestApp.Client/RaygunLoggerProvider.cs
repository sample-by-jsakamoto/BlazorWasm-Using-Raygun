using Raygun.Blazor;

namespace RaygunTestApp.Client;

public class RaygunLoggerProvider : ILoggerProvider
{
    private readonly RaygunBlazorClient _raygunClient;

    public RaygunLoggerProvider(RaygunBlazorClient raygunClient)
    {
        this._raygunClient = raygunClient;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new RaygunLogger(categoryName, this._raygunClient);
    }

    public void Dispose()
    {
    }
}