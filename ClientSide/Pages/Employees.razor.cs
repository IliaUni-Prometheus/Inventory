using ClientSide.Models;

namespace ClientSide.Pages
{
    public partial class Employees
    {
        private List<EmployeeViewModel> _employees = null;

        protected override async Task OnInitializedAsync()
        {
            _employees = await EmployeeService.All();
        }
    }
}
