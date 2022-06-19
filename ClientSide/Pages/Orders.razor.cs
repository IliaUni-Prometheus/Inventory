using ClientSide.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;

namespace ClientSide.Pages
{
    public partial class Orders
    {
        private List<OrderViewModel> _orders = null;
        private int _currentPage = 1;
        private int _pageCount = 0;

        private int _startPageIndex = 1;
        private int _endPageIndex = 1;

        protected override async Task OnInitializedAsync()
        {
            await FetchOrders(_currentPage);

            NavigationManager.LocationChanged += HandleLocationChanged;
        }

        private async void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            int page = int.Parse(QueryHelpers.ParseQuery(uri.Query)["page"]);
            await FetchOrders(page);
            StateHasChanged();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= HandleLocationChanged;
        }

        private async Task FetchOrders(int page)
        {
            var response = await OrderService.All(page);
            _currentPage = response.CurrentPage;
            _pageCount = response.Pages;
            _orders = response.Items;
            SetPaginationIndexes();
        }

        private void SetPaginationIndexes()
        {
            _startPageIndex = (_currentPage - 5) < 1 ? 1 : _currentPage - 5;
            _endPageIndex = (_currentPage + 5) > _pageCount ? _pageCount : _currentPage + 5;
            if (_endPageIndex < 10 && _pageCount >= 10) { _endPageIndex = 10; }
        }
    }
}
