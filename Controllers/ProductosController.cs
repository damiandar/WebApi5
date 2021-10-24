using Microsoft.AspNetCore.Mvc;
using ProyRepositorio.Models;
using ProyRepositorio.Repositorios;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using ProyRepositorio.Helpers;

namespace ProyRepositorio.Controllers
{
    public class ProductosController : ControladorBase<Producto>
    {
        public readonly ProductoRepositorio<ComercioDbContext> _context;
        private readonly ILogger<Producto> _logger;
        private readonly IUrlHelper _urlHelper;

        public ProductosController(ILogger<Producto> logger, ProductoRepositorio<ComercioDbContext> context ) :base(context)
        {
            _context = context;
            _logger = logger;
           // _urlHelper = injectedUrlHelper;
        }

        [HttpGet]
        [Route("sinstock")]
        public List<Producto> MostrarRopa()
        {
            //return _context.Productos.Include(x=>x.Categoria).ToList();
            //_context.BuscarTodos(x => x.Categoria, x => x.Vendedor);
            return _context.BuscarTodos(x=> x.Categoria).ToList(); 
        }

        /// <summary>
        /// Busqueda por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>Devuelve un objeto del tipo Producto</returns>
        [HttpGet("busqueda/{nombre}", Name = nameof(MostrarPorNombre))]
        //[Produces("application/json")] //Salida:content-type
        //[Consumes("application/json")] //Accept indicates what kind of response from the server the client can accept
        public ActionResult<Producto> MostrarPorNombre(string nombre)
        {
            //var resultado = Ordenador<Producto>.Ordenar(_context.BuscarPor(x => x.Id > 0), orden);
            //var listapag = ListaPaginada<Producto>.Paginar(resultado, nropag, tampag);
            var prodbuscado = _context.BuscarPor(x => x.Nombre == nombre);
            if (prodbuscado == null)
            {
                return NotFound("El producto no fue encontrado con ese nombre: " + nombre);
            }
            var prod = prodbuscado.First();
            //prod = this.CrearLinks(prod);
            return prod;
        }

        [HttpGet("busquedaporvalor/{precio:decimal}")]
        public ActionResult<List<Producto>> MostrarPorNombre(decimal precio)
        {
            var ListaPorPrecio = _context.BuscarPor(x => x.Precio > precio).ToList();
            if (ListaPorPrecio == null || ListaPorPrecio.Count() == 0)
            {
                return NotFound("No hay productos con precio mayor a " + precio);
            }
            return ListaPorPrecio;
        }

        [HttpGet("/api/Mensajes/{Numero:int:min(1000)}")]
        public ActionResult MostrarMensaje(int Numero)
        {
            return Ok("Este es el numero: " + Numero);
        }

        [HttpGet("/api/Mensajes/{Mensaje:minlength(2)}")]
        public ActionResult MostrarMensaje(string Mensaje)
        {
            return Ok("Este es el mensaje: " + Mensaje);
        }

        [HttpGet("/api/Avisos/{nro:int?}")]
        public ActionResult MostrarAviso(int nro = 1033)
        {
            return Ok("Este es el aviso: " + nro);
        }

        [HttpGet("/api/Llamadas/{nro:int=999}/clientes/{clienteid}")]
        public ActionResult MostrarAviso2(int nro)
        {
            return Ok("Este es el aviso: " + nro);
        }

        [HttpGet("busquedaporcategoria")]
        public ActionResult MostrarProductosPorCategoria(int categoriaId)
        {
            return Ok("estos son los productos con este id de categoria:" + categoriaId);
        }

        //HTTPGET api/productos todos los productos
        //HTTPPOST api/productos insertar
        //HTTPDELETE api/productos/12 borrar 

        ///HTTP Get api/Productos/134/clientes/  lista de clientes que compraron el prod 134
        ///HTTP Get api/Productos/134/clientes/87 bool el cliente 87 compro el prod 134 o 
        // datos del cliente 87 que compro el prod 134

        [HttpGet("api/notas/{idalumno}")]
        public ActionResult MostrarNotas(int categoriaId)
        {
            return Ok("estos son los productos con este id de categoria:" + categoriaId);
        }

        //HATEOAS
        //Hypermedia as the Engine of Application State
        //{  "id":134,
        // "nombre": "zapatilla
        //  "cliente": [  {id:87 , name: laura 
        //                  "links": [  { "info": "para ver los datos del cliente",
        //                                 "href": "api/clientes/87"
        //                               ]    
        //}  ]
        //}
        private Producto CrearLinks(Producto user)
        {
            var idObj = new { id = user.Id };
            //user.Links.Add(
            //    new LinkDto(this._urlHelper.Link(nameof(this.GetAll), idObj),
            //    "self",
            //    "GET"));
            //user.Links.Add(
            //    new LinkDto(this._urlHelper.Link(nameof(this.MostrarPorNombre), idObj),
            //    "self",
            //    "GET"));

            //user.Links.Add(
            //    new LinkDto(this._urlHelper.Link(nameof(this.ModifyHateoasUser), idObj),
            //    "update_user",
            //    "PUT"));

            //user.Links.Add(
            //    new LinkDto(this._urlHelper.Link(nameof(this.PartiallyModifyHateoasUser), idObj),
            //    "partially_update_user",
            //    "PATCH"));

            //user.Links.Add(
            //    new LinkDto(this._urlHelper.Link(nameof(this.DeleteHateoasUser), idObj),
            //    "delete_user",
            //    "DELETE"));

            return user;
        }

    }
}