using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyRepositorio.Models;

namespace ProyRepositorio.Repositorios
{
    public class CategoriaRepositorio<TContext> : RepositoryBase<Categoria,TContext> where TContext:DbContext
    {
        public CategoriaRepositorio(TContext context) : base(context)
        {

        }

    }
}
