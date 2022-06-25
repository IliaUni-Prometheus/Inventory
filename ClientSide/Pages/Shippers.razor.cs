using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Shippers
    {
        private List<ShipperViewModel> _shippers = null;

        protected override async Task OnInitializedAsync()
        {
            this._shippers = await ShipperService.All();
        }
    }
}
