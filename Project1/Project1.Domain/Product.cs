﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Domain
{
    /// <summary>
    ///  Provides data fields representing a particular product
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public int Id{ get; set; } //not used currently, name used as db identifier
        private static int _Id = 1;

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if(value <= 0)
                {
                    _quantity = 0;
                }
                else
                {
                    _quantity = value;
                }
            }
        }

        //TODO:check for valid quantity when calling constructor
        public Product(string name, int initialQuantity,double price)
        {
            this.Name = name;
            this.Price = price;
            this._quantity = initialQuantity;
            this.Id = _Id;
            ++_Id;
        }
        public Product()
        {
        }

    }
}
