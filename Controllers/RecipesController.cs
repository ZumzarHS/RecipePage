using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipePage.Data;
using RecipePage.Models;
using RecipePage.Models.Entities;

namespace RecipePage.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public RecipesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeViewModel viewModel)
        {
            try
            {
                var recipe = new Recipe
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ListOfIngredients = viewModel.ListOfIngredients,
                    CookingTime = viewModel.CookingTime
                };
                // Add new Recipe object to database
                await dbContext.Recipes.AddAsync(recipe);
                await dbContext.SaveChangesAsync();
                return View();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var recipes = await dbContext.Recipes.ToListAsync();

            return View(recipes);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var recipe = await dbContext.Recipes.FindAsync(id);
            return View(recipe);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Recipe viewModel)
        {
            var recipe = await dbContext.Recipes.FindAsync(viewModel.Id);

            if (recipe is not null)
            {
                recipe.Id = viewModel.Id;
                recipe.Title = viewModel.Title;
                recipe.Description = viewModel.Description;
                recipe.ListOfIngredients = viewModel.ListOfIngredients;
                recipe.CookingTime = viewModel.CookingTime;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Recipes");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Recipe viewModel)
        {
            var recipe = await dbContext.Recipes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (recipe is not null)
            {
                dbContext.Recipes.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Recipes");
        }
    }
}