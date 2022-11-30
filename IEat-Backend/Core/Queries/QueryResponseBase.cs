namespace Core.Queries
{
    public abstract class QueryResponseBase
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}