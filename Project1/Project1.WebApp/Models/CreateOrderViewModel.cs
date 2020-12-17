using Project1.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.Models
{
    public class CreateOrderViewModel
    {
        //input needed to choose a store
        
        public List<string> ProductNames { get; set; }
        
        [Required]
        public List<int> Quantities { get; set; }
    }
}
