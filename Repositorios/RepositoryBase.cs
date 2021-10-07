using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyRepositorio.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProyRepositorio.Repositorios
{
    public abstract class RepositoryBase<T,TContext> : IRepository<T> where T : ModeloBase where TContext :DbContext
    {
        protected readonly TContext _context;

        public RepositoryBase(TContext context)
        {
            _context = context;
        }
        public virtual void Actualizar(T entidad)
        {
            this._context.Set<T>().Update(entidad);
            this._context.SaveChanges();
        }

        public virtual void Borrar(int id)
        {
            //entorno desconectado 
            //var entidad= this._context.Set<T>().Find(id);
            var entidad = this.FindById(id);
            this._context.Set<T>().Attach(entidad);
            this._context.Set<T>().Remove(entidad);
            this._context.SaveChanges();
        }

        public virtual void Crear(T entidad)
        {
            this._context.Set<T>().Add(entidad);
            this._context.SaveChanges();
        }

        public virtual List<T> FindAll()
        {
            return this._context.Set<T>().ToList();
        }

        public virtual T FindById(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        public IQueryable<T> BuscarPor(Expression<Func<T, bool>> query)
        {
            return _context.Set<T>().Where(query); 
        }
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query;
        }
        //public List<T> BuscarListaPor(Expression<Func<T, bool>> query)
        //{
        //    return _context.Set<T>().Where(query).ToList();
        //}
    }
}
