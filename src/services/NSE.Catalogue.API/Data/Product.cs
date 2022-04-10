using System;
using NSE.Core;

namespace NSE.Catalogue.API.Data
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public string Value { get; set; }
        public DateTime RegistryDate { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}