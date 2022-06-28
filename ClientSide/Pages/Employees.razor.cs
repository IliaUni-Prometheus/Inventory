using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTOs;

namespace ClientSide.Pages
{
    public partial class Employees
    {
        private List<EmployeeDTO> _employees = null;
        private int _currentPage = 1;
        private int _pageCount = 0;

        private string[] _columnNames = new string[] { "First Name", "Last Name", "Country", "Postal Code" };
        private string[] _propertyNames = new string[] { "FirstName", "LastName", "Country", "PostalCode" };

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
            var response = await EmployeeService.GetEmployees(page);
            _currentPage = response.CurrentPage;
            _pageCount = response.Pages;
            _employees = response.Orders;
        }
    }
}
