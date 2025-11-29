using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;
public class CompositeLoggerConfiguration
{
    public IEnumerable<ILogger> Loggers;
}