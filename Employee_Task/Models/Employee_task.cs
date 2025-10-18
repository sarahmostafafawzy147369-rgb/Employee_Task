using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Task.Models
{
    public class Employee_task
    {
        [Key]
        public int TaskID { get; set; } 
        public string? Title { get; set; }   
        public string? Description { get; set; } 
        public string? Status { get; set; }  
        public DateTime DueDate   { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]      
        public Employee ?Employee { get; set; }
    }
}
