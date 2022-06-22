using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Products
    {
        private List<ProductViewModel> _products = null;

        protected override async Task OnInitializedAsync()
        {
            this._products = await ProductService.All();
        }
    }
}
