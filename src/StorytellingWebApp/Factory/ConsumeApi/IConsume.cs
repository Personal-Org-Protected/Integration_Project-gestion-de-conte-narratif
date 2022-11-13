using StorytellingWebApp.Models;

namespace StorytellingWebApp.Factory.ConsumeApi
{
    public interface IConsume<T> where T : class
    {
        Task<PaginatedItems<T>> GetAll(string chemin);
        Task<T> GetAllClient(string chemin);
        Task<T> GetAll();
        Task<T> GetById(string chemin);
        Task<T> Insert(T model);
        Task<Result> InsertWithResult(T model);
        Task<T> Update(string chemin, T model);
        Task Delete(string chemin);
        string ImplementPath(IDictionary<string,int> parameters);
    }
}
