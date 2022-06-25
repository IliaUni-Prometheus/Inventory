using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Regions
    {
        private List<RegionViewModel> _regions = null;

        protected override async Task OnInitializedAsync()
        {
            this._regions = await RegionService.All();
        }
    }
}
