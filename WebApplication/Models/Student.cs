using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required*")]

        public string Phone { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Department")]
        public int DepID { get; set; }

  



        [NotMapped]
        public string Department { get; set; }


    }
}

