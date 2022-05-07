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


