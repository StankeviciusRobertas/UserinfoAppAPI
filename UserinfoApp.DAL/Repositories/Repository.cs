using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.DAL.Repositories
{
    // Abstract base class for generic repository operations
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        // Protected fields to store the database context and DbSet for the entity type
        protected readonly UserinfoAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(UserinfoAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Virtual method to get all entities of type T from the database
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        // Virtual method to get an entity of type T by its id from the database
        public virtual T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        // Virtual method to add a new entity of type T to the database
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        // Virtual method to update an existing entity of type T in the database
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Virtual method to delete an entity of type T from the database
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
