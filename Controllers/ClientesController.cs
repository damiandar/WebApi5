using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyRepositorio.Repositorios;
using ProyRepositorio.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyRepositorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //public readonly CategoriaRepositorio<ComercioDbContext> _context;

        //public ClientesController(CategoriaRepositorio<ComercioDbContext> context)
        //{
        //    _context = context;
        //}

        public readonly  ComercioDbContext  _context;

        public ClientesController( ComercioDbContext  context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Producto> MostrarTodos()
        {
            return _context.Productos
                .Include(x=>x.Categoria)
                .ToList();
        }
    }
}
