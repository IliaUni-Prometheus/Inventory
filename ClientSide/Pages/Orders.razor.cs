using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTOs;
using System.Web;

namespace ClientSide.Pages
{
    public partial class Orders
    {
        [Parameter]
        public string? page { get; set; }

        private List<OrderDTO> _orders = null;
        private int _currentPage = 1;
        private int _pageCount = 0;
        private int _pageSize = 10;


        private string[] _columnNames = new string[] { "Order Date", "Freight", "Ship Address", "Postal Code" };
        private string[] _propertyNames = new string[] { "OrderDate", "Freight", "ShipAddress", "ShipPostalCode" };

        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await FetchOrders(_currentPage, _pageSize);

                NavigationManager.LocationChanged += HandleLocationChanged;

                StateHasChanged();
            }
        }

        private async void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            var page = HttpUtility.ParseQueryString(uri.Query).Get("page");

            if (int.TryParse(page, out int pageValue))
            {
                await FetchOrders(pageValue, _pageSize);
            }

            StateHasChanged();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= HandleLocationChanged;
        }

        private async Task FetchOrders(int page, int pageSize)
        {
            var response = await OrderService.GetOrders(page, pageSize);
            _currentPage = response.CurrentPage;
            _pageCount = response.Pages;
            _orders = response.Data;
        }
    }
}
