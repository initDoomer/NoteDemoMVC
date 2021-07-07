using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusType { get; set; }
    }
}
