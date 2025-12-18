using Raygun.Blazor;

namespace RaygunTestApp.Client;

public class RaygunLogger : ILogger
{
    private readonly string _categoryName;

    private readonly RaygunBlazorClient _raygunClient;

    public RaygunLogger(string categoryName, RaygunBlazorClient raygunClient)
    {
        this._categoryName = categoryName;
        this._raygunClient = raygunClient;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    // ErrorとCriticalのみRaygunに送信
    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Error;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!this.IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        exception ??= new Exception(message);
        _ = this._raygunClient.RecordExceptionAsync(exception, userCustomData: new Dictionary<string, object>
            {
                { "LogLevel", logLevel.ToString() },
                { "Category", this._categoryName },
                { "Message", message },
                { "EventId", eventId.ToString() }
            });
    }
}