namespace MovieDatabase.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using MovieDatabase.Data.Common.Models;

    public interface ICrudService<T> where T : BaseDeletableModel<string>
    {
        IQueryable<T> GetAll();

        Task<T> Get(string id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete (string id);

        Task<bool> Exists(string name);
    }
}