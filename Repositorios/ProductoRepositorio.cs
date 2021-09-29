using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyRepositorio.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyRepositorio.Repositorios
{
    public class ProductoRepositorio<TContext> : RepositoryBase<Producto,TContext> where TContext :DbContext
    {
        public ProductoRepositorio(TContext context) : base(context)
        {

        }

   

        public override List<Producto> FindAll()
        {
            return this.Incluir().ToList();
        }

        private IQueryable<Producto> Incluir()
        {
            return _context.Set<Producto>()
            .Include(p => p.Categoria);
        }
    }
}
