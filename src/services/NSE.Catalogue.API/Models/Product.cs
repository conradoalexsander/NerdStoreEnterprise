using System;
using NSE.Core.DomainObjects;

namespace NSE.Catalogue.API.Models
{
    public class Product : Entity, IAggregateRoot
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