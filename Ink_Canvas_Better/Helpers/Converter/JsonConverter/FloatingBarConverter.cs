using System;
using System.Collections.Generic;
using System.Text;
using Ink_Canvas_Better.Controls;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Helpers.Converter.JsonConverter
{
    internal class FloatingBarConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FloatingBar);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.StartObject)
            {
                FloatingBar floatingBarGroup = new();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        return floatingBarGroup;
                    }
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = reader.Value!.ToString()!;
                        reader.Read();
                        switch (propertyName)
                        {
                            case "Orientation":
                                floatingBarGroup.Orientation = (Orientation)Enum.Parse(typeof(Orientation), reader.Value!.ToString()!);
                                break;
                            case "Spacing":
                                floatingBarGroup.Spacing = Convert.ToDouble(reader.Value);
                                break;
                            case "CornerRadius":
                                floatingBarGroup.CornerRadius = (CornerRadius)serializer.Deserialize(reader, typeof(CornerRadius))!;
                                break;
                        }
                    }
                }
            }

            throw new JsonSerializationException("Unexpected token when deserializing FloatingBar.");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
