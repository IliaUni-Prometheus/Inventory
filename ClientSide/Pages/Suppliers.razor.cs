using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Suppliers
    {
        private List<SupplierViewModel> _suppliers = null;

        protected override async Task OnInitializedAsync()
        {
            this._suppliers = await SupplierService.All();
        }
    }
}
