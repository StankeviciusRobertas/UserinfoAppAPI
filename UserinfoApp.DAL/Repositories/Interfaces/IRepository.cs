
using System.Net.Mime;

namespace UserinfoApp.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T? Get(int id);
        IQueryable<T> GetAll();
        void Update(T entity);
    }
}