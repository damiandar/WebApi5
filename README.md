# Proyecto web api .net core 5

Repositorio

## Versionado

```
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
```
Startup.cs
```
services.AddApiVersioning(o => {
        o.ReportApiVersions = true;
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(3, 0);
});
```
En cada controlador puedo poner esto
```
[Route("[controller]/{version:apiVersion}")]
```
y probarlo

http://localhost:5000/weatherforecast/3.0

Tambien podemos probar poniendo el decorador
```
[ApiVersion("1.0")]
[ApiVersion("2.0")]
```

## Middleware para manejar exceptions de toda la app

Creo el middleware y agrego en startup 

```
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
```

```
        app.UseMiddleware< WebApi5.Middlewares.ExcepcionesMiddleware>();
```

## Agregar token en JWT, agrega el botón authorize arriba

```
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", 
        new OpenApiInfo { Title = "ProyRepositorio", Version = "v1" }
    );
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
                    In=ParameterLocation.Header,
                    Description="Por favor ingrese el JWT",
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
         new OpenApiSecurityScheme{Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme, Id="Bearer"}},Array.Empty<string>()
        }
    }); 
});

```

## Problem Details 

https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/
