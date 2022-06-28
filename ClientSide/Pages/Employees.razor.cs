using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTOs;
using System.Web;

namespace ClientSide.Pages
{
    public partial class Employees
    {
        private List<EmployeeDTO> _employees = null;
        private int _currentPage = 1;
        private int _pageCount = 0;
        private int _pageSize = 10;


        private string[] _columnNames = new string[] { "First Name", "Last Name", "Country", "Postal Code" };
        private string[] _propertyNames = new string[] { "FirstName", "LastName", "Country", "PostalCode" };

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
            var response = await EmployeeService.GetEmployees(page, pageSize);
            _currentPage = response.CurrentPage;
            _pageCount = response.Pages;
            _employees = response.Data;
        }
    }
}
