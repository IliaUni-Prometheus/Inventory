namespace ClientSide.Models
{
    public class PaginatedResultViewModel<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
