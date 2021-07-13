using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserActivityTracking.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
