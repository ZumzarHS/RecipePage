using System.ComponentModel.DataAnnotations;

namespace RecipePage.Models.Entities
{
    public class Ingredients
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Amount { get; set; }
        private string _units = "";
        public string Units { get { return _units; } set { _units = value; } }
        private string _note = "";
        public string Note { get { return _note; } set { _note = value; } }
    }
}