using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Config;
using Microsoft.EntityFrameworkCore;

namespace AwesomeBankAPI.Repository
{
    public class SqlGenericRepository<T> where T : class 
    {
        protected readonly AwesomeBankDbContext _context;

        public SqlGenericRepository(AwesomeBankDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            List<T> list;
            IQueryable<T> dbQuery = _context.Set<T>();
            list = dbQuery.ToList();
            return list;
        }

        public virtual T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            T item = null;
            IQueryable<T> dbQuery = _context.Set<T>();
            item = dbQuery.FirstOrDefault(predicate); //Apply where clause
            return item;
        }

        public virtual IEnumerable<T> GetMultiple(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> item = null;
            IQueryable<T> dbQuery = _context.Set<T>();
            item = dbQuery.Where(predicate); //Apply where clause
            return item;
        }

        public virtual T GetById(Guid id)
        {
            T item = null;
            item = _context.Set<T>().Find(id);
            return item;
        }

        public int Add(T model)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Add(model);
                    var response = _context.SaveChanges();
                    dbContextTransaction.Commit();

                    return response;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public int Update(T model)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Entry(model).State = EntityState.Modified;
                    var response = _context.SaveChanges();
                    dbContextTransaction.Commit();

                    return response;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public int Remove(Guid Id)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var model = _context.Accounts.FirstOrDefault(x => x.Id == Id);
                    _context.Entry(model).State = EntityState.Deleted;
                    var response = _context.SaveChanges();
                    dbContextTransaction.Commit();

                    return response;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
