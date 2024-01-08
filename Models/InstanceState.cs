using System.Text.Json.Serialization;

namespace JsonSerializeTests.Models
{
    public class TableState
    {
        public readonly Dictionary<Guid, RowState> RowStates;

        public TableState()
        {
            RowStates = new Dictionary<Guid, RowState>();
        }
    }
}
