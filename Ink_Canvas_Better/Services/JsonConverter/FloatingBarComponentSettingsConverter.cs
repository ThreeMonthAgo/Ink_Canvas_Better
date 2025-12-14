using System;
using System.Collections.Generic;
using System.Text;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ink_Canvas_Better.Services.JsonConverter
{
    public class FloatingBarComponentSettingsConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(IFloatingBarComponentSettingBase).IsAssignableFrom(objectType);

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jobj = JObject.Load(reader);

            if (!jobj.TryGetValue("ComponentGuid", out JToken? guidToken))
            {
                throw new JsonSerializationException("ComponentGuid is required for deserialization");
            }

            string guid = guidToken.ToString();

            if (!App.RegisteredControls.TryGetValue(guid, out Type? type))
            {
                throw new JsonSerializationException($"Component with guid {{{guid}}} is not registered");
            }

            IFloatingBarComponentSettingBase instance = App.GetService(type) as IFloatingBarComponentSettingBase;

            jobj.Remove("ComponentGuid");

            using (var jsonReader = jobj.CreateReader())
            {
                serializer.Populate(jsonReader, instance);
            }

            return instance;
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
                catch (Exception)
                {
                    writer.WritePropertyName(p1.Name);
                    writer.WriteNull();
                }
            }
            writer.WriteEndObject();
        }
    }
}
