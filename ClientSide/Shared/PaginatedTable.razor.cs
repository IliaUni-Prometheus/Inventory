using Microsoft.AspNetCore.Components;

namespace ClientSide.Shared
{
    public partial class PaginatedTable<T>
    {
        [Parameter]
        public string[] ColumnNames { get; set; }
        [Parameter]
        public string[] PropertyNames { get; set; }
        [Parameter]
        public List<T> Items { get; set; }
        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public int PageCount { get; set; }
        [Parameter]
        public string Route { get; set; }

        private int _startPageIndex = 1;
        private int _endPageIndex = 1;


        protected override void OnParametersSet()
        {
            _startPageIndex = (CurrentPage - 5) < 1 ? 1 : CurrentPage - 5;
            _endPageIndex = (CurrentPage + 5) > PageCount ? PageCount : CurrentPage + 5;
            if (_endPageIndex < 10 && PageCount >= 10) { _endPageIndex = 10; }
        }
    }
}
