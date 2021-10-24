using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProyRepositorio.Models;
using ProyRepositorio.Repositorios;
using ProyRepositorio.Helpers;
using System;
using Microsoft.AspNetCore.Http;

namespace ProyRepositorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControladorBase<T> : ControllerBase where T: ModeloBase
    {
        public readonly IRepository<T> _repo;
        public ControladorBase(IRepository<T> repo)
        {
            _repo = repo;
        }


        [HttpGet("{nropag?}/{tampag?}")]
        public ActionResult<List<T>> GetAll(string orden = "", int nropag = 1, int tampag = 10)
        {
            try
            {
                var resultado = Ordenador<T>.Ordenar(_repo.BuscarPor(x => x.Id > 0), orden);
                var listapag = ListaPaginada<T>.Paginar(resultado, nropag, tampag);
                var metadatos = new
                {
                    listapag.TotalPaginas,
                    listapag.TotalReg,
                    listapag.PagActual,
                    listapag.TieneAnt,
                    listapag.TieneProx,
                    listapag.TamPag
                };
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadatos));
                // add links on each retrieved user
                return Ok(listapag);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error en el servidor, vuelva a intentar en otro momento");
            }
        }


        [HttpGet("{id}")]
        public virtual ActionResult<T> GetPorId(int id){
            var resultado = _repo.FindById(id);
            return Ok(resultado);
        }

        [HttpPost]
        public virtual ActionResult<T> Nuevo(T entidad ){
            _repo.Crear(entidad);
            return Ok();
        }
        [HttpPut]
        public virtual ActionResult<T> Modificar(T entidad)
        {
            _repo.Actualizar(entidad);
            return Ok();
        }


    }
}