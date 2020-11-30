using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class StoreEntity
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Location { get; set; }

        //could add customer list, would require link table
        public List<OrderEntity> Orders { get; set; }
        public List<InventoryEntity> Stocks { get; set; }
    }
}
