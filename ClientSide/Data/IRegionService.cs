using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IRegionService
    {
        Task<List<RegionViewModel>> All();
    }
}
