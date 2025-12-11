using System.Collections.Concurrent;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ink_Canvas_Better.Services
{
    /// <summary>
    /// Converter
    /// </summary>
    public class ControlsService(ILogger<ControlsService> logger) : JsonConverter
    {
        private readonly ILogger<ControlsService> logger = logger;

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

            if (!Program.RegisteredControls.TryGetValue(guid, out Type? type))
            {
                throw new JsonSerializationException($"Component with guid {{{guid}}} is not registered");
            }

            object instance = Program.GetService(type);

            jobj.Remove("ComponentGuid");

            using (var jsonReader = jobj.CreateReader())
            {
                serializer.Populate(jsonReader, instance);
            }

            return instance;
        }
    }
}
