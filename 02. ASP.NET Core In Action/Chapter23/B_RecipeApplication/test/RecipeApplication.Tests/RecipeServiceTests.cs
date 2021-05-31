namespace RecipeApplication.Tests
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    using RecipeApplication.Data;
    using RecipeApplication.Models;

    using System.Threading.Tasks;

    using Xunit;

    public class RecipeServiceTests
    {
        [Fact]
        public async Task CreateRecipe_InsertsNewRecipe()
        {
            const string recipeName = "Test Recipe";

            // The in-memory database is destroyed when the connection is closed.
            // await closes the connection
            await using var connection = new SqliteConnection("DataSource=:memory:");

            connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            // Run the test against one instance of the context
            await using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                var service = new RecipeService(context);

                var cmd = new CreateRecipeCommand
                {
                    Name = recipeName,
                };

                var user = new ApplicationUser
                {
                    Id = 123.ToString()
                };

                await service.CreateRecipe(cmd, user);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new AppDbContext(options))
            {
                Assert.Equal(1, await context.Recipes.CountAsync());

                var recipe = await context.Recipes.SingleAsync();
                Assert.Equal(recipeName, recipe.Name);
            }
        }

        [Fact]
        public async Task GetRecipeDetails_CanLoadFromContextAsync()
        {
            await using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            // Insert seed data into the database using one instance of the context
            await using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.Recipes.AddRangeAsync(
                    new Recipe { RecipeId = 1, Name = "Recipe1" },
                    new Recipe { RecipeId = 2, Name = "Recipe2" },
                    new Recipe { RecipeId = 3, Name = "Recipe3" });
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new AppDbContext(options))
            {
                var service = new RecipeService(context);

                var recipe = await service.GetRecipeDetail(2);

                Assert.NotNull(recipe);
                Assert.Equal(2, recipe.Id);
                Assert.Equal("Recipe2", recipe.Name);
            }
        }

        [Fact]
        public async Task GetRecipeDetails_DoesNotLoadDeletedRecipeAsync()
        {
            const string recipeName = "Test Recipe";
            const int recipeId = 2;

            await using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            // Insert seed data into the database using one instance of the context
            await using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.Recipes.AddAsync(new Recipe { RecipeId = 1, Name = "Recipe1" });
                await context.Recipes.AddAsync(new Recipe { RecipeId = 2, Name = recipeName, IsDeleted = true });
                await context.Recipes.AddAsync(new Recipe { RecipeId = 3, Name = "Recipe3" });
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new AppDbContext(options))
            {
                var service = new RecipeService(context);

                var recipe = await service.GetRecipeDetail(recipeId);

                Assert.Null(recipe);
            }
        }
    }
}
