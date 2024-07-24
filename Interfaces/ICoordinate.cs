using MyselfStaj2.Classes;
using MyselfStaj2.Services;

namespace MyselfStaj2.Interfaces
{
    public interface ICoordinate
    {
        Response GetCoordinate();
        Response GetCoordinateById(int id);
        Response InsertCoordinate(Coordinate c);
        Response DeleteCoordinate(int id);

        Response UpdateCoordinate(int id, Coordinate c);
    }
}
