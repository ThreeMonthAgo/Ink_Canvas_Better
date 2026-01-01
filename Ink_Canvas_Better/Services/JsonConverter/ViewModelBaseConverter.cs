using System;
using System.Diagnostics;
using System.Reflection;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ink_Canvas_Better.Services.JsonConverter;

public class ViewModelBaseConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType) => typeof(ViewModelBase).IsAssignableFrom(objectType);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jobj = JObject.Load(reader);
        if (!jobj.TryGetValue("Guid", out JToken? guidToken))
        {
            Debug.WriteLine($"{objectType}");
            throw new JsonSerializationException("Guid is required for deserialization");
        }
        string guid = guidToken.ToString();
        if (!IApp.GetService<ComponentService>().RegisteredComponents.TryGetValue(guid, out Type type))
        {
            throw new JsonSerializationException($"Component with guid {{{guid}}} is not registered");
        }
        var instance = ActivatorUtilities.CreateInstance(IApp.GetService<IServiceProvider>(), type);
        jobj.Remove("Guid");
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
        // write Guid
        var componentAttribute = value.GetType().GetCustomAttribute<ComponentAttribute>();
        if (componentAttribute != null)
        {
            var guid = componentAttribute.Guid;
            writer.WritePropertyName("Guid");
            writer.WriteValue(guid);
        }
        // write other properties
        var p = value.GetType().GetProperties();
        foreach (var p1 in p)
        {
            // Skip properties with [JsonIgnore] attribute
            if (p1.GetCustomAttribute<JsonIgnoreAttribute>() != null) continue;
            // write
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
