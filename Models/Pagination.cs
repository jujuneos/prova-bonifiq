namespace ProvaPub.Models
{
    public class Pagination
    {
        public List<dynamic> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
