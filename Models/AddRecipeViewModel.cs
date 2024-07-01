using RecipePage.Models.Entities;

namespace RecipePage.Models
{
    public class AddRecipeViewModel
    {
        private string _title = "";
        public required string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title cannot be null or empty.");
                }
                _title = value;
            }
        }

        // FUTURE WORK: Implement image of dish

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private List<string> _ingredients = new List<string>();
        public required List<string> ListOfIngredients
        {
            get { return _ingredients; }
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("Ingredients cannot be null or empty.");
                }
                _ingredients = value;
            }
        }


        /*  private List<Ingredients> _ingredients = new List<Ingredients>();
          public required List<Ingredients> ListOfIngredients
          {
              get { return _ingredients; }
              set
              {
                  if (value == null || value.Count == 0)
                  {
                      throw new ArgumentException("Ingredients cannot be null or empty.");
                  }
                  _ingredients = value;
              }
          } */
        // TODO: property that containts information on how to

        public TimeSpan CookingTime { get; set; }
    }
}