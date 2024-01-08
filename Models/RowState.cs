namespace JsonSerializeTests.Models
{
    public class RowState
    {
        public Dictionary<string, object?> Records { get; set; }

        public RowState()
        {
            Records = new Dictionary<string, object?>();
        }
    }
}
