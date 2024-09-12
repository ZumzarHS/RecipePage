namespace RecipePage.Models.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
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
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                _description = value;
            }
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

        private List<string> _instructions = new List<string>();
        public required List<string> Instructions
        {
            get { return _instructions; }
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("Instructions cannot be null or empty.");
                }
                _instructions = value;
            }
        }

        public TimeSpan CookingTime { get; set; }
    }
}