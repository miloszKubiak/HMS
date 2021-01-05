using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }


        [Column(TypeName = "varchar(50)")]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }


        [Column(TypeName = "varchar(11)")]
        [DisplayName("PESEL")]
        [Required(ErrorMessage = "This field is required.")]
        public string Pesel { get; set; }


        [Column(TypeName = "varchar(50)")]
        [DisplayName("Position")]
        [Required(ErrorMessage = "This field is required.")]
        public string Position { get; set; }


        [Column(TypeName = "varchar(50)")]
        [DisplayName("Specialization")]
        public string? Specialization { get; set; }


        [Column(TypeName = "varchar(7)")]
        [DisplayName("PWZ Number")]
        public string? PwzNumber { get; set; }

        
    }
}
