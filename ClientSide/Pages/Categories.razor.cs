using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Categories
    {
        private List<CategoryViewModel> _categories = null;

        protected override async Task OnInitializedAsync()
        {
            this._categories = await CategoryService.All();
        }
    }
}
