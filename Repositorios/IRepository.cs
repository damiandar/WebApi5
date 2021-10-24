using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProyRepositorio.Repositorios
{
    public interface IRepository<T>
    {
       T FindById(int id);
        IQueryable<T> BuscarTodos(params Expression<Func<T, object>>[] includes);
        void Crear(T entidad);

        void Actualizar(T entidad);
        IQueryable<T> BuscarPor(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] includes);
        void Borrar(int id);
    }
}
