using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
