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
            if (QueryHelpers.ParseQuery(uri.Query).ContainsKey("page"))
            {
                int page;
                if (int.TryParse(QueryHelpers.ParseQuery(uri.Query)["page"], out page))
                {
                    await FetchOrders(page);
                }
            }

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
            _startPageIndex = _currentPage - 4;
            if (_startPageIndex < 1) { _startPageIndex = 1; }
            if (_pageCount - 9 > 1 && _startPageIndex > _pageCount - 9) { _startPageIndex = _pageCount - 9; }
            if (_pageCount <= 10) { _startPageIndex = 1; }
            _endPageIndex = _currentPage + 6;
            if (_endPageIndex < 11 && _pageCount >= _endPageIndex)
            {
                if (_pageCount >= 10) { _endPageIndex = 11; }
                else { _endPageIndex = _pageCount + 1; }
            }
            if (_endPageIndex > _pageCount) { _endPageIndex = _pageCount + 1; }
        }
    }
}
