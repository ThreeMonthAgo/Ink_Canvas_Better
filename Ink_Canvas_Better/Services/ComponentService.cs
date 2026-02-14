using System.Collections.Concurrent;
using System.Reflection;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Utilities.Attributes;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Services;

public class ComponentService(ILogger<ComponentService> logger)
{
    private readonly ILogger<ComponentService> logger = logger;

    public ConcurrentDictionary<string, Type> RegisteredComponents { get; } = [];

    /// <summary>
    /// registers all components marked with the ComponentAttribute in the current AppDomain assemblies.
    /// </summary>
    public void DetectAndRegisterComponents()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var viewModelTypes = assembly.GetTypes().Where(t => t.GetCustomAttribute<ComponentAttribute>() != null);
            foreach (var viewModelType in viewModelTypes)
            {
                var componentAttribute = viewModelType.GetCustomAttribute<ComponentAttribute>();
                if (componentAttribute != null)
                {
                    var viewType = componentAttribute.ViewType;
                    var guid = componentAttribute.Guid;
                    var r = RegisteredComponents.TryAdd(guid, viewModelType);
                    if (r)
                    {
                        DataTemplateHelper.RegisterDataTemplate(viewModelType, viewType);
                        logger.WriteLog(LogLevel.Trace, () => $"Data template registered (guid:{guid} viewmodel:{viewModelType.Name} view:{viewType.Name})");
                    }
                    else
                    {
                        logger.WriteLog(LogLevel.Warning, () => $"Component with guid {guid} has already registered.");
                    }
                }
            }
        }
    }
}
