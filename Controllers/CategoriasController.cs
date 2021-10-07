using ProyRepositorio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProyRepositorio.Repositorios;
using System.Linq;

namespace ProyRepositorio.Controllers
{
    public class CategoriasController : ControladorBase<Categoria>
    {

        public readonly CategoriaRepositorio<ComercioDbContext> _context;

        public CategoriasController(CategoriaRepositorio<ComercioDbContext> context) : base(context)
        {
            _context = context;
        }

        //api/categorias/133/productos
        [HttpGet]
        [Route("{idcategoria}/productos")]
        public ActionResult<List<Categoria>> MostrarProductos(int idcategoria){
            //var resultado = _context.Filtrar(x => x.Id == idcategoria, x => x.Productos, x => x.Vendedor);
            return _context.Filtrar(x => x.Id == idcategoria).ToList();
        }

    }
}