using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactor_BusinessServices.Entities
{
    public class ProductEntity
    {
        /// <summary>
        /// Unique Product ID as GUID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product Name as string
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Description as string
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product Price as decimal
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Product Delivery Price as decimal
        /// </summary>
        public decimal DeliveryPrice { get; set; }
        
    }
}
