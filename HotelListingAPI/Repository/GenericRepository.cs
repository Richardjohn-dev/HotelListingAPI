using HotelListingAPI.Data;
using HotelListingAPI.IRepository;
using HotelListingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListingAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        // private readonly Databa

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            // includes parameter - configure the lazy loading. 
            // if we have a hotel, include the country also for example
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression); // can be a lambda expression as a parameter
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
           
            if (expression != null)
            {
                query = query.Where(expression);
            }
            // attach extra
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            // order
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;                      
         
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
        
            return await query.AsNoTracking()
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
           await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity); // adds it to the efcore watch to track changes
            _context.Entry(entity).State = EntityState.Modified; // notify entity framework tracker/watcher
        }
    }
}
