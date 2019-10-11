using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CityComparison.EntityFrameworkCore.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private CityComparisonContext _context;

        public EntityBaseRepository(CityComparisonContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T Get(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }

            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }

            return query;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Deleted;
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }
    }
}
