using System.Collections.Concurrent;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ink_Canvas_Better.Services
{
    /// <summary>
    /// Provides registration, instantiation, and json serialization support for floating bar controls
    /// </summary>
    public class ControlsService(IServiceProvider serviceProvider, ILogger<ControlsService> logger) : JsonConverter
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;
        private readonly ILogger<ControlsService> logger;

        public ConcurrentDictionary<string, Type> RegisteredControls = new();

        public bool TryRegisterControl<T>(string guid)
        {
            return RegisteredControls.TryAdd(guid,typeof(T));
        }

        public bool UnregisterControl(string guid, out Type? type)
        {
            return RegisteredControls.TryRemove(guid, out type);
        }

        /// <summary>
        /// Creates an instance of a floating bar component setting based on the specified unique identifier.
        /// </summary>
        /// <remarks>The returned instance is created using dependency injection via the provided service
        /// provider. If the specified <paramref name="guid"/> does not match any registered component, the method
        /// returns <see langword="null"/>.</remarks>
        public IFloatingBarComponentSettingBase CreateControl(string guid)
        {
            RegisteredControls.TryGetValue(guid, out Type type);
            var c = ActivatorUtilities.CreateInstance(serviceProvider, type) as IFloatingBarComponentSettingBase;
            return c;
        }
        
        /// <summary>
        /// Attempts to create a floating bar control instance associated with the specified GUID.
        /// </summary>
        /// <remarks>This method uses the registered service provider to instantiate the control. If the
        /// GUID does not correspond to a known control type, no instance is created and the method returns
        /// false.</remarks>
        public bool TryCreateControl(string guid, out IFloatingBarComponentSettingBase? control)
        {
            RegisteredControls.TryGetValue(guid, out Type type);
            if (type == null)
            {
                control = null;
                return false;
            }
            else
            {
                control = ActivatorUtilities.CreateInstance(serviceProvider, type) as IFloatingBarComponentSettingBase;
                return true;
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IFloatingBarComponentSettingBase).IsAssignableFrom(typeToConvert);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();
            var p = typeof(IFloatingBarComponentSettingBase).GetProperties();
            foreach (var p1 in p)
            {
                try
                {
                    var v = p1.GetValue(value);
                    writer.WritePropertyName(p1.Name);
                    serializer.Serialize(writer, v);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Error serializing property {p1.Name}: {ex}");
                    writer.WritePropertyName(p1.Name);
                    writer.WriteNull();
                }
            }
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jobj = JObject.Load(reader);

            if (!jobj.TryGetValue("ComponentGuid", out JToken? guidToken))
            {
                throw new JsonSerializationException("ComponentGuid is required for deserialization");
            }

            string guid = guidToken.ToString();

            if (!RegisteredControls.TryGetValue(guid, out Type? type))
            {
                throw new JsonSerializationException($"Component with guid {{{guid}}} is not registered");
            }

            object instance = ActivatorUtilities.CreateInstance(serviceProvider, type)
                ?? throw new JsonSerializationException($"Failed to create instance of {type.Name}");

            jobj.Remove("ComponentGuid");

            using (var jsonReader = jobj.CreateReader())
            {
                serializer.Populate(jsonReader, instance);
            }

            return instance;
        }
    }
}
