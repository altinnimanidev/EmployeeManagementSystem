using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("position")]
        public string Position { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 10000000, ErrorMessage = "Salary must be between 0 and 10,000,000")]
        [Column("salary")]
        public decimal Salary { get; set; }
    }
}
