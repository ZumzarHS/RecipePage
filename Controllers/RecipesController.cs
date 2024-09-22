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
                    CookingTime = viewModel.CookingTime,
                    Instructions = viewModel.Instructions
                };

                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.ImageFile.CopyToAsync(memoryStream);
                        recipe.ImageData = memoryStream.ToArray(); // Convert the image to byte array
                    }
                }
                else
                {
                    recipe.ImageData = null; // If no image was uploaded
                }


                // Add new Recipe object to database
                await dbContext.Recipes.AddAsync(recipe);
                await dbContext.SaveChangesAsync();
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the recipe.");
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

            // Map Recipe entity to EditRecipeViewModel
            var viewModel = new EditRecipeViewModel
            {
                Title = recipe.Title,
                Description = recipe.Description,
                ListOfIngredients = recipe.ListOfIngredients,
                CookingTime = recipe.CookingTime,
                Instructions = recipe.Instructions,
                ExistingImage = recipe.ImageData  // Pass existing image (byte array)
            };

            return View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditRecipeViewModel viewModel)
        {
            var recipe = await dbContext.Recipes.FindAsync(viewModel.Id);

            if (recipe is not null)
            {
                recipe.Title = viewModel.Title;
                recipe.Description = viewModel.Description;
                recipe.ListOfIngredients = viewModel.ListOfIngredients;
                recipe.CookingTime = viewModel.CookingTime;
                recipe.Instructions = viewModel.Instructions;

                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.ImageFile.CopyToAsync(memoryStream);
                        recipe.ImageData = memoryStream.ToArray();  // Store the new image
                    }
                }
                else
                {
                    // Keep the existing image if no new image is uploaded
                    recipe.ImageData = viewModel.ExistingImage;
                }

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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var recipe = await dbContext.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
    }
}