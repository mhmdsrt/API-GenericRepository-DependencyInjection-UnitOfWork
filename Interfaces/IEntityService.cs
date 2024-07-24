using MyselfStaj2.Classes;

namespace MyselfStaj2.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Response GetAllCoordinate();
        Response GetCoordinateById(int id);
        Response InsertCoordinate(int id, T entity);
        Response UpdateCoordinate(int id, T entity);

        Response DeleteCoordinate(int id);
    }
}
