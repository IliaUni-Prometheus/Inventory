using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IShipperService
    {
        Task<List<ShipperViewModel>> All();
    }
}
