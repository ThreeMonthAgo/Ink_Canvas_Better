using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ink_Canvas_Better.Interface;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services.JsonConverter
{
    public class FloartingBarCollectionConverter : JsonConverter<ObservableCollection<IFloatingBarComponentSettingBase>>
    {
        public override ObservableCollection<IFloatingBarComponentSettingBase>? ReadJson(JsonReader reader, Type objectType, ObservableCollection<IFloatingBarComponentSettingBase>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (hasExistingValue && existingValue != null)
                {
                    existingValue.Clear();
                }
                return null;
            }
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonSerializationException($"Expected StartArray, got {reader.TokenType}");
            }
            ObservableCollection<IFloatingBarComponentSettingBase> targetCollection;

            if (hasExistingValue && existingValue != null)
            {
                targetCollection = existingValue;
                targetCollection.Clear();
            }
            else
            {
                targetCollection = [];
            }

            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var item = serializer.Deserialize<IFloatingBarComponentSettingBase>(reader);

                if (item != null)
                {
                    targetCollection.Add(item);
                }
                reader.Read();
            }

            return targetCollection;
        }

        public override void WriteJson(JsonWriter writer, ObservableCollection<IFloatingBarComponentSettingBase>? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartArray();

            foreach (var item in value)
            {
                serializer.Serialize(writer, item);
            }

            writer.WriteEndArray();
        }
    }
}
