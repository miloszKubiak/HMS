using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Duty
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public DateTime DutyDate { get; set; }
    }
}
