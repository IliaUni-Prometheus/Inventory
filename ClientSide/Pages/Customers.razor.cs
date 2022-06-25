using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Customers
    {
        private List<CustomerViewModel> _customers = null;

        protected override async Task OnInitializedAsync()
        {
            this._customers = await CustomerService.All();
        }
    }
}
