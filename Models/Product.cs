using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Remote("CheckProductCode","Home", ErrorMessage = "Product Code already exits")]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Vendor { get; set; }
        public int CategoryID { get; set; }
        public int WarehouseID { get; set; }

        public Category Category { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
