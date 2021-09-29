using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using ProyRepositorio.Models;
using System.Security.Claims;

namespace ProyRepositorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            IActionResult response = Unauthorized();
            //validar usuario ir a base y chekear datos correctos
            Usuario usuariovalidado = usuario;
            if (usuariovalidado != null)
            {
                var tokenString = GenerarJWT(usuariovalidado);
                response = Ok(new {
                    token= tokenString,
                    datosusuario=usuariovalidado
                });
            }
            return response;
        }


        private string GenerarJWT(Usuario usuariovalidado )
        {
            var clavesecreta = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credenciales = new SigningCredentials(clavesecreta, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usuariovalidado.Nombre),
                new Claim("nombrecompleto",usuariovalidado.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credenciales
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
