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