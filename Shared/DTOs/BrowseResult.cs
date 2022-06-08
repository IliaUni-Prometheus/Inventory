namespace Shared.DTOs
{
    public class BrowseResult<T>
    {
        public List<T> Orders { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
