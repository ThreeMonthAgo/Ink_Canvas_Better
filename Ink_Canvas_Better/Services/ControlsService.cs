using System.Collections.Concurrent;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ink_Canvas_Better.Services
{
    /// <summary>
    /// Provides registration, instantiation, and json serialization support for floating bar controls
    /// </summary>
    public class ControlsService(IServiceProvider serviceProvider) : JsonConverter
    {
        IServiceProvider serviceProvider = serviceProvider;

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
            writer.WriteStartObject();
            var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead &&
                           p.GetCustomAttribute<JsonIgnoreAttribute>() == null &&
                           p.Name != "ComponentGuid");

            foreach (var item in properties)
            {
                writer.WritePropertyName(item.Name);
                writer.WriteValue(item.GetValue(value));
            }

            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            object target;
            if (jsonObject.TryGetValue("ComponentGuid", out JToken guid))
            {
                string s = guid.ToString();
                if (RegisteredControls.TryGetValue(s, out Type? value))
                {
                    target = value;
                }
                else
                {
                    throw new JsonReaderException($"Component with guid {{{s}}} unregistered");
                }
            }
            else throw new JsonReaderException();
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }
    }
}
