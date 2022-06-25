using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Territories
    {
        private List<TerritoryViewModel> _territories = null;

        protected override async Task OnInitializedAsync()
        {
            this._territories = await TerritoryService.All();
        }
    }
}
