using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.DataLayer.Data
{
    public class Entity<T> : IEntity<T> where T : class
    {
        private LootLoOnlineDatabaseEntities LootLoOnlineDatabaseEntities { get; set; }
        internal DbSet<T> dbSet;
        public Entity()
        {
            LootLoOnlineDatabaseEntities = DatabaseConnection.Entityinstance;
            this.dbSet = LootLoOnlineDatabaseEntities.Set<T>();
        }
        public virtual async Task<List<T>> List()
        {
            try
            {
                return dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<IList<T>> List(int? page, int? pageSize, System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, object>> sort)
        {
            var result = dbSet.AsQueryable();

            if (predicate != null)
                result = result.Where(predicate);

            if (sort != null)
                result = result.OrderBy(sort);

            if (page.HasValue && pageSize.HasValue)
                result = result.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return result.ToList();
        }
        public virtual async Task<int> Add(T item)
        {
            try
            {
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    dbSet.Add(item);
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> BulkAdd(List<T> items)
        {
            try
            {
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    dbSet.AddRange(items);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual T GetSingle(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                T item = null;
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    item = dbQuery
                        .AsNoTracking() //Don't track any changes for the selected item
                        .FirstOrDefault(where); //Apply where clause
                }
                return item;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public virtual async Task<int> Update(T item)
        {
            try
            {
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    dbSet.Attach(item);
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> Delete(T item)
        {
            try
            {
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    dbSet.Remove(item);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> BulkDelete(List<T> items)
        {
            try
            {
                using (var context = LootLoOnlineDatabaseEntities)
                {
                    dbSet.RemoveRange(items);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
