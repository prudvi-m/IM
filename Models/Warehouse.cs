using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Warehouse
    {
        [Key]
        public int ID { get; set; }

        [Remote("CheckWarehouse","Home",ErrorMessage ="Already exits")]
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }

        public IList<Product> Products { get; set; }
    }
}
