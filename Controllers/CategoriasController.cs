using ProyRepositorio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProyRepositorio.Repositorios;


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
        public List<Producto> MostrarProductos(int idcategoria){
            return null;
        }

    }
}