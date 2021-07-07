using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Models
{
    public class Note
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title{ get; set; }

        public string Message { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } // this create a foreign key relation with the category model/table using the categoryId field


        [Display(Name = "Status Type")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status{ get; set; } // this create a foreign key relation with the status model/table using the statusId field



    }
}
