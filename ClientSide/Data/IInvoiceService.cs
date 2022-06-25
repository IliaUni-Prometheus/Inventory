using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IInvoiceService
    {
        Task<List<InvoiceViewModel>> All();
    }
}
