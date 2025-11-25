using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Helpers.Converter.JsonConverter
{
    internal class FloatingBarGroupConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
