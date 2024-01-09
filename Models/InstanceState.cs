using System.Text.Json.Serialization;

namespace JsonSerializeTests.Models
{
    public class TableState
    {
        public Dictionary<Guid, RowState> RowStates {get; set;}

        public TableState()
        {
            RowStates = new Dictionary<Guid, RowState>();
        }
    }
}
