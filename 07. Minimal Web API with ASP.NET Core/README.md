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
