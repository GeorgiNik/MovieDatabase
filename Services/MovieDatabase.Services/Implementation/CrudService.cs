namespace MovieDatabase.Services.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Models;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;

    public class CrudService<T> : ICrudService<T>
        where T : BaseDeletableModel<string>
    {
        private IDeletableEntityRepository<T> data;

        public CrudService(IDeletableEntityRepository<T> data)
        {
            this.data = data;
        }

        public IQueryable<T> GetAll()
        {
            var result = this.data.All();
            
            return result;
        }

        public async Task<T> Get(string id)
        {
            var entity = await this.data.GetByIdAsync(id);
            
            return entity;
        }

        public async Task<T> Create(T entity)
        {
            this.data.Add(entity);
            await this.data.SaveChangesAsync();
            
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            this.data.Update(entity);
            await this.data.SaveChangesAsync();
            
            return entity;
        }
        
        public async Task<bool> Delete (string id)
        {
            var entity = await this.data.GetByIdAsync(id);
            
            if (entity != null)
            {
                this.data.Delete(entity);
                await this.data.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        public Task<bool> Exists(string name)
        {
            var exists =  this.data.All().AnyAsync(x => x.Name == name);
            
            return exists;
        }
    }
}