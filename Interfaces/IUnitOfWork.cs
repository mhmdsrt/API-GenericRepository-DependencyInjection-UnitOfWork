using MyselfStaj2.Interfaces;

namespace MyselfStaj2.Entity
{
    public interface IUnitOfWork:IDisposable 
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
    }
}
