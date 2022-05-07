# Minimal WEB API with ASP.NET Core

Minimal APIs are architected to create HTTP APIs with minimal dependencies. They are ideal for microservices and apps that want to include only the minimum files, features, and dependencies in ASP.NET Core.


## Overview

This tutorial creates the following API:

| API                        	| Description                 	| Request body 	| Response body        	|
|----------------------------	|-----------------------------	|--------------	|----------------------	|
| GET /                      	| Browser test, "Hello World" 	| None         	| Hello World!         	|
| GET /todoitems             	| Get all to-do items         	| None         	| Array of to-do items 	|
| GET /todoitems/complete    	| Get completed to-do items   	| None         	| Array of to-do items 	|
| GET /todoitems/{id}        	| Get an item by ID           	| None         	| To-do item           	|
| POST /todoitems            	| Add a new item              	| To-do item   	| To-do item           	|
| PUT /todoitems/{id}        	| Update an existing item     	| To-do item   	| None                 	|
| DELETE /todoitems/{id}     	| Delete an item              	| None         	| None                 	|


## Differences between minimal APIs and APIs with controllers

- No support for filters: For example, no support for [IAsyncAuthorizationFilter](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iasyncauthorizationfilter), [IAsyncActionFilter](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iasyncactionfilter), [IAsyncExceptionFilter](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iasyncexceptionfilter), [IAsyncResultFilter](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iasyncresultfilter), and [IAsyncResourceFilter](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iasyncresourcefilter).
- No support for model binding, i.e. [IModelBinderProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.imodelbinderprovider), [IModelBinder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.imodelbinder). Support can be added with a custom binding shim.
  - No support for binding from forms. This includes binding [IFormFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile).
- No built-in support for validation, i.e. [IModelValidator](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.validation.imodelvalidator)
- No support for [application parts](https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-6.0) or the [application model](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/application-model?view=aspnetcore-6.0). There's no way to apply or build your own conventions.
- No built-in view rendering support. We recommend using [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start?view=aspnetcore-6.0) for rendering views.
- No support for [JsonPatch](https://www.nuget.org/packages/Microsoft.AspNetCore.JsonPatch/)
- No support for [OData](https://www.nuget.org/packages/Microsoft.AspNetCore.OData/)
- No support for [ApiVersioning](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning/). See [this issue](https://github.com/dotnet/aspnet-api-versioning/issues/751) for more details.


## The following code creates a [WebApplication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication) (app) without explicitly creating a [WebApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder)

```csharp
var app = WebApplication.Create(args);

app.MapGet("/", () => "Hello World!");

app.Run();
```

[WebApplication.Create](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.create) initializes a new instance of the [WebApplication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication) class with preconfigured defaults.


## Working with ports

When a web app is created with Visual Studio or dotnet new, a Properties/launchSettings.json file is created that specifies the ports the app responds to. The following sections set the port the app responds to.

```csharp
var app = WebApplication.Create(args);

app.MapGet("/", () => "Hello World!");

app.Run("http://localhost:3000");
```

In the preceding code, the app responds to port 3000.


## Multiple sockets

In the following code, the app responds to port 3000 and 4000.


```csharp
var app = WebApplication.Create(args);

app.Urls.Add("http://localhost:3000");
app.Urls.Add("http://localhost:4000");

app.MapGet("/", () => "Hello World");

app.Run();
```


## Set the port from the command line

The following command makes the app respond to port 7777:

```bash
dotnet run --urls="https://localhost:7777"
```


## Read the port from environment

The following code reads the port from the environment:

```csharp
var app = WebApplication.Create(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.MapGet("/", () => "Hello World");

app.Run($"http://localhost:{port}");
```

The preferred way to set the port from the environment is to use the ASPNETCORE_URLS environment variable, which is shown in the following section.


## Set the ports via the ASPNETCORE_URLS environment variable

The ASPNETCORE_URLS environment variable is available to set the port:

```
ASPNETCORE_URLS=http://localhost:3000
```

ASPNETCORE_URLS supports multiple URLs:

```
ASPNETCORE_URLS=http://localhost:3000;https://localhost:5000
```


## Listen on all interfaces

The following samples demonstrate listening on all interfaces


### http://*:3000

```csharp
var app = WebApplication.Create(args);

app.Urls.Add("http://*:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```


### http://+:3000

```csharp
var app = WebApplication.Create(args);

app.Urls.Add("http://+:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```


### http://0.0.0.0:3000

```csharp
var app = WebApplication.Create(args);

app.Urls.Add("http://0.0.0.0:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```


### Listen on all interfaces using ASPNETCORE_URLS

The preceding samples can use ASPNETCORE_URLS

```
ASPNETCORE_URLS=http://*:3000;https://+:5000;http://0.0.0.0:5005
```


## Specify HTTPS with development certificate

```csharp
var app = WebApplication.Create(args);

app.Urls.Add("https://localhost:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```

For more information on the development certificate, see [Trust the ASP.NET Core HTTPS development certificate on Windows and macOS](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-6.0#trust).


## Specify HTTPS using a custom certificate

The following sections show how to specify the custom certificate using the **appsettings.json** file and via configuration.

### Specify the custom certificate with appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Kestrel": {
    "Certificates": {
      "Default": {
        "Path": "cert.pem",
        "KeyPath": "key.pem"
      }
    }
  }
}
```

### Specify the custom certificate via configuration

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure the cert and the key
builder.Configuration["Kestrel:Certificates:Default:Path"] = "cert.pem";
builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = "key.pem";

var app = builder.Build();

app.Urls.Add("https://localhost:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```


### Use the certificate APIs

```csharp
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        var certPath = Path.Combine(builder.Environment.ContentRootPath, "cert.pem");
        var keyPath = Path.Combine(builder.Environment.ContentRootPath, "key.pem");

        httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath, 
                                         keyPath);
    });
});

var app = builder.Build();

app.Urls.Add("https://localhost:3000");

app.MapGet("/", () => "Hello World");

app.Run();
```


## Read the environment

```csharp
var app = WebApplication.Create(args);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/oops");
}

app.MapGet("/", () => "Hello World");
app.MapGet("/oops", () => "Oops! An error happened.");

app.Run();
```


## Configuration

The following code reads from the configuration system:

```csharp
var app = WebApplication.Create(args);

var message = app.Configuration["HelloKey"] ?? "Hello";

app.MapGet("/", () => message);

app.Run();
```
