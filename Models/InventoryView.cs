namespace InventorySystem.Models
{
    public class InventoryView
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Warehouse> Warehouses { get; set; }
        public IList<Product> Products { get; set; }

        public Product Product { get; set; }
    }
}
