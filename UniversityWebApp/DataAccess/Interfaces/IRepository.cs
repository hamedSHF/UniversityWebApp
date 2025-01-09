namespace PracticeWebApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll();
        Task<T> GetByUserName(string userName);
    }
}
