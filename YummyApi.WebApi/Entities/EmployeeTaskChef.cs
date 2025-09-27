using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YummyApi.WebApi.Entities
{
    public class EmployeeTaskChef
    {
        [Key]
        public int Id { get; set; }   // Primary Key (tek kolon) – kolaylık olsun diye ekledim

        [ForeignKey("EmployeeTask")] // Foreign Key olduğunu belirtiyoruz
        public int EmployeeTaskId { get; set; } // Foreign Key kolonu
        public EmployeeTask EmployeeTask { get; set; } // Navigation property

        [ForeignKey("Chef")] // Foreign Key olduğunu belirtiyoruz
        public int ChefId { get; set; } // Foreign Key kolonu
        public Chef Chef { get; set; } // Navigation property

        // Ek alan istersen buraya ekle
       // public string RoleInTask { get; set; }  // Örneğin, "Lead Chef", "Assistant Chef" gibi
    }
}
