using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProyRepositorio.Models;
using ProyRepositorio.Repositorios;


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
        [HttpGet]
        public virtual ActionResult<List<T>> GetTodos(){
            var resultado = _repo.FindAll();
            //return NotFound();
            return Ok(resultado.ToList());
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