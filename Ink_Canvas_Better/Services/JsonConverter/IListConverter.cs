using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services.JsonConverter
{
    /// <summary>
    /// Use this JsonConverter to ensure that all types implementing IList are deserialized by overriding rather than appending.
    /// </summary>
    public class IListConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(IList).IsAssignableFrom(objectType);

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var c = existingValue as IList;
            c.Clear();
            serializer.Populate(reader, c);
            return c;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var item in (IList)value)
            {
                serializer.Serialize(writer, item);
            }
            writer.WriteEndArray();
        }
    }
}
