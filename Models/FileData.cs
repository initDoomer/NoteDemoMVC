using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Models
{
    public class FileData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageCaption { set; get; }

        [Required]
        public string ImageDescription { set; get; }

        public string ImageName { get; set; }


        [NotMapped]
        public IFormFile ImageFileName { set; get; }

    }
}
