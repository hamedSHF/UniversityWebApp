namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll();
        Task<int> SaveChanges();
        Task<int> CountAll();
    }
}
