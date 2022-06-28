namespace Shared.DTOs
{
    public class BrowseResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public ErrorDetails ErrorDetails { get; set; }
    }
}
