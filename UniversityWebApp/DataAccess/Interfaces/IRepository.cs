namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChanges();
        Task<int> CountAll();
    }
}
