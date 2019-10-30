using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AMA.DataLayer.Data
{
    public class Entity<T> : IEntity<T> where T : class
    {
        private LootLoOnlineDatabaseEntities _lootLoOnlineDatabaseEntities;

        public Entity()
        {
            _lootLoOnlineDatabaseEntities = DatabaseConnection.Entityinstance;

        }
        public virtual async Task<List<T>> GetAll()
        {
            try
            {
                return _lootLoOnlineDatabaseEntities.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<T>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderExpression = null)
        {
            var result = _lootLoOnlineDatabaseEntities.Set<T>().AsQueryable();

            if (predicate != null)
                result = result.Where(predicate);

            if (orderExpression != null)
                result = orderExpression(result);


            return result.ToList();
        }
        public virtual async Task<List<T>> GetAllByFilter(int? page, int? pageSize, System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, object>> sort)
        {
            var result = _lootLoOnlineDatabaseEntities.Set<T>().AsQueryable();

            if (predicate != null)
                result = result.Where(predicate);

            if (sort != null)
                result = result.OrderBy(sort);

            if (page.HasValue && pageSize.HasValue)
                result = result.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return result.ToList();
        }
        public virtual T Get(Func<T, bool> where,
         params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                T item = null;
                using (var context = new LootLoOnlineDatabaseEntities())
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

        public virtual async Task<int> Add(T item)
        {
            try
            {
                using (var context = new LootLoOnlineDatabaseEntities())
                {

                    context.Set<T>().Add(item);
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
                using (var context = new LootLoOnlineDatabaseEntities())
                {
                    context.Set<T>().AddRange(items);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Value cannot be null"))
                {
                    using (var context = new LootLoOnlineDatabaseEntities())
                    {
                        foreach (var T in items)
                        {
                            try
                            {
                                context.Set<T>().Add(T);
                            }
                            catch (Exception)
                            {
                                context.SaveChanges();
                            }
                        }
                        context.SaveChanges();
                    }
                }
                else if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {

                    var se = ex.InnerException.InnerException as SqlException;
                    var code = se.Number;
                    if (se.Number == 2627)
                    {
                        foreach (var T in items)
                        {
                            try
                            {
                                this.Update(T);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }

            }
            finally
            {

            }

            return true;
        }

        public virtual async Task<int> Update(T item)
        {
            try
            {
                using (var context = new LootLoOnlineDatabaseEntities())
                {
                    context.Set<T>().Attach(item);
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
                using (var context = new LootLoOnlineDatabaseEntities())
                {
                    context.Set<T>().Remove(item);
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
                using (var context = new LootLoOnlineDatabaseEntities())
                {
                    // context.Set<T>().SqlQuery("exec [Flipkart].[RemoveOldOfferProducts]");
                    context.RemoveOldOfferProducts();
                    //context.SaveChanges();
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
