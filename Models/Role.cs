using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Account> Accounts { get; set; }
    }
}
