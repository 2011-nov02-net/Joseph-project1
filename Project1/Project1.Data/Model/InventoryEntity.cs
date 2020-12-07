using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project1.Data.Model
{
    public class InventoryEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //foriegn keys and navigation properties
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public StoreEntity Store { get; set; }
        
        public ProductEntity Product { get; set; }
    }
}
