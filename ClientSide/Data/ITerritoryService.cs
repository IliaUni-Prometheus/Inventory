using ClientSide.Models;

namespace ClientSide.Data
{
    public interface ITerritoryService
    {
        Task<List<TerritoryViewModel>> All();
    }
}
