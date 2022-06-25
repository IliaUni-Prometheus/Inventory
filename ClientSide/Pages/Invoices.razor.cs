using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Invoices
    {
        private List<InvoiceViewModel> _invoices = null;

        protected override async Task OnInitializedAsync()
        {
            this._invoices = await InvoiceService.All();
        }
    }
}
