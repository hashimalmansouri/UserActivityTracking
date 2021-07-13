using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserActivityTracking.Data
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Data { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
