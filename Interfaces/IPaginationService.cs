namespace ProvaPub.Interfaces
{
    public interface IPaginationService
    {
        dynamic List(List<dynamic> items, int page);
    }
}
