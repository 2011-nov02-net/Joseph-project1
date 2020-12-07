using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression("[A-Z].*")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression("[A-Z].*")]
        public string LastName { get; set; }
    }
}
