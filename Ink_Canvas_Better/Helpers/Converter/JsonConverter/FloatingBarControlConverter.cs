using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Helpers.Converter.JsonConverter
{
    internal class FloatingBarControlConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.BaseType == typeof(FloatingBarControlBase);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndArray)
                    {
                        return null;
                    }
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        reader.Read();
                        var propertyName = reader.Value.ToString();
                        Control? control = AppHost.GetService<ControlsService>().CreateControl(Guid.Parse(reader.Value.ToString()));
                        return control;
                    }
                }
            }

            throw new JsonSerializationException("Unexpected token when deserializing FloatingBarControl.");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
