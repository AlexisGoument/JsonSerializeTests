using System.Text.Json;
using JsonSerializeTests.Models;

namespace JsonSerialize.Tests
{
    public static class JsonSerializer
    {
        public static string Serialize(object item)
        {
            return System.Text.Json.JsonSerializer.Serialize(item);
        }

        public static byte[] SerializeToUtf8Bytes(object item)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(item);
        }

        public static T? Deserialize<T>(string json)
        {
            var item = System.Text.Json.JsonSerializer.Deserialize<T>(json);

            switch (item)
            {
                case TableState tableState:
                    foreach (var kvp in tableState.RowStates) {
                        ConvertRowEventRecords(kvp.Value.Records);
                    }
                    break;
                case RowState rowState:
                    ConvertRowEventRecords(rowState.Records);
                    break;
            }

            return item;
        }

        private static void ConvertRowEventRecords(Dictionary<string, object?> records)
        {
            foreach (var record in records)
            {
                if (record.Value is JsonElement element)
                {
                    switch (element.ValueKind)
                    {
                        case JsonValueKind.Object:
                            records[record.Key] = element.GetString();
                            break;
                        case JsonValueKind.Array:
                            records[record.Key] = element.GetString();
                            break;
                        case JsonValueKind.String:
                            records[record.Key] = element.GetString();
                            break;
                        case JsonValueKind.Number:
                            if (element.TryGetInt32(out int value))
                                records[record.Key] = value;
                            else
                                records[record.Key] = element.GetDouble();
                            break;
                        case JsonValueKind.True:
                            records[record.Key] = true;
                            break;
                        case JsonValueKind.False:
                            records[record.Key] = false;
                            break;
                        case JsonValueKind.Null:
                            records[record.Key] = null;
                            break;
                        case JsonValueKind.Undefined:
                            records[record.Key] = null;
                            break;
                        default:
                            records[record.Key] = element.GetString();
                            break;
                    }
                }
            }
        }
    }
}
