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


                