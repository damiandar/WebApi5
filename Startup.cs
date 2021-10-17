using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProyRepositorio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProyRepositorio.Repositorios;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ProyRepositorio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProyRepositorio", Version = "v1" });
            });

            services.AddDbContext<ComercioDbContext>( 
                o => o.UseSqlServer(Configuration.GetConnectionString("ComercioConnectionString")));
            
            services.AddCors(c => {
                c.AddPolicy(
                    "PermitirOrigin",
                    o => o.AllowAnyOrigin()
                ); 
                //c.AddPolicy(name:
                //    "PermitirSoloOrigenRegistrado",
                //    o =>
                //    {
                //        o.WithOrigins("http://localhost:5000")
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //    }
                //);
                //services.AddCors(c=> {
                //    c.AddPolicy(name: "PermitirTodos", o => { o.WithOrigins("http://localhost:5000")
                //        .AllowAnyHeader()
                //        .WithMethods("GET"); } );
                //    c.AddPolicy(name: "PermitirSoloServerCentral", o => { o.WithOrigins("http://localhost:5001")
                //        .AllowAnyHeader()
                //        .AllowAnyMethod(); });
                //    c.AddPolicy(name: "PermitirMinisterioEconomia", o => {
                //        o.WithOrigins("http://localhost:5002")
                //        .WithHeaders( "Content-Type","Accept")
                //        .AllowAnyMethod(); });
                //    c.AddPolicy(name: "PermitirHost", o => {
                //        o.WithOrigins("http://localhost:5003")
                //        .WithHeaders("X-Pagination")
                //        .WithMethods("GET");
                //    });
                //});
            });
            
            services.AddScoped<ProductoRepositorio<ComercioDbContext>, ProductoRepositorio<ComercioDbContext>>();
            services.AddScoped<CategoriaRepositorio<ComercioDbContext>, CategoriaRepositorio<ComercioDbContext>>();
    
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience=Configuration["Jwt:Audience"],
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
                    };
                });

                //services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
                //{
                //    var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                //    return new UrlHelper(actionContext);
                //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyRepositorio v1"));
                //habilito cors para todos
                app.UseCors("PermitirOrigin");
            }

            app.UseRouting();
            
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
