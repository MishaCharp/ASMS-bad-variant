using ASMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Repositories
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository()
        {
            _context = ApplicationContext.Instance;
            _dbSet = _context.Set<T>();
        }

        public T? GetById(int id) => _dbSet.FirstOrDefault(x => x.Id == id);
        public IEnumerable<T> GetAll() => _dbSet.AsEnumerable();
        public bool Create(T item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool Remove(T item)
        {
            try
            {
                var removedItem = _dbSet.FirstOrDefault(item);
                if (removedItem != null) return false;

                _dbSet.Remove(removedItem);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool Update(T item)
        {
            try
            {
                var updatedItem = _dbSet.Find(item.Id);
                if (updatedItem == null) return false;

                updatedItem.Update(item);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
    public static class Repositories
    {
        public static Repository<User> UserRepository = new Repository<User>();
        public static Repository<Role> RoleRepository = new Repository<Role>();
    }
}
