using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
