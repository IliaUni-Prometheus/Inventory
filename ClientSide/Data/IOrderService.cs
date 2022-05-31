namespace ClientSide.Data
{
    public interface IOrderService
    {
        Task<List<object>> All();
        Task<object> OfId(int orderId);
        Task Delete(int orderId);
    }
}
