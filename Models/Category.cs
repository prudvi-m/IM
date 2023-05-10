using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Remote("CheckName", "Home", ErrorMessage ="Name already exists.")]
        public string CategoryName { get; set; }

        public IList<Product> Products { get; set; }

    }
}
