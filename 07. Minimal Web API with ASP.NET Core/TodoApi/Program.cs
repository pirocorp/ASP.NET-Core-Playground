namespace TodoApi
{
    using Microsoft.EntityFrameworkCore;

    using TodoApi.Data;
    using TodoApi.Data.Models;
    using TodoApi.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var app = ConfigureWebApplication(args);

            RegisterEndpoints(app);

            // Runs the app.
            app.Run();
        }

        private static WebApplication ConfigureWebApplication(string[] args)
        {
            // Creates a WebApplicationBuilder with preconfigured defaults
            // Configure Services in DI Container
            var builder = WebApplication.CreateBuilder(args);

            // Adds the database context to the dependency injection (DI) container
            builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
            
            // Enables displaying database-related exceptions
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Creates a WebApplication with preconfigured defaults
            // Configure the HTTP request pipeline.
            var app = builder.Build();

            return app;
        }

        private static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Creates an HTTP GET endpoint / which returns Hello World!
            app.MapGet("/", () => "Hello World!");

            app.MapGet("/todoitems", async (TodoDb db) =>
                await db.Todos
                    .Select(x => new TodoItemDTO(x))
                    .ToListAsync());

            app.MapGet("/todoitems/complete", async (TodoDb db) =>
                await db.Todos
                    .Where(t => t.IsComplete)
                    .Select(x => new TodoItemDTO(x))
                    .ToListAsync());

            app.MapGet("/todoitems/{id:int}", async (int id, TodoDb db) =>
                await db.Todos.FindAsync(id)
                    is { } todo
                    ? Results.Ok(new TodoItemDTO(todo))
                    : Results.NotFound());

            // Creates an HTTP POST endpoint /todoitems to add data to the in-memory database
            app.MapPost("/todoitems", async (TodoItemDTO todoItemDto, TodoDb db) =>
            {
                var todoItem = new Todo
                {
                    Name = todoItemDto.Name,
                    IsComplete = todoItemDto.IsComplete
                };

                db.Todos.Add(todoItem);
                await db.SaveChangesAsync();

                return Results.Created($"/todoitems/{todoItemDto.Id}", new TodoItemDTO(todoItem));
            });

            app.MapPut("/todoitems/{id:int}", async (int id, TodoItemDTO inputTodo, TodoDb db) =>
            {
                var todo = await db.Todos.FindAsync(id);

                if (todo is null)
                {
                    return Results.NotFound();
                }

                todo.Name = inputTodo.Name;
                todo.IsComplete = inputTodo.IsComplete;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/todoitems/{id:int}", async (int id, TodoDb db) =>
            {
                if (await db.Todos.FindAsync(id) is not { } todo)
                {
                    return Results.NotFound();
                }

                db.Todos.Remove(todo);
                await db.SaveChangesAsync();

                return Results.Ok(new TodoItemDTO(todo));
            });
        }
    }
}
