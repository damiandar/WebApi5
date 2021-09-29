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
        List<T> FindAll();
        void Crear(T entidad);

        void Actualizar(T entidad);
        public IQueryable<T> BuscarPor(Expression<Func<T, bool>> query);
        void Borrar(int id);
    }
}
