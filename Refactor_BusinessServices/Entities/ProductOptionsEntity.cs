using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactor_BusinessServices.Entities
{
   public class ProductOptionsEntity
    {
        /// <summary>
        /// Unique Option ID as GUID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product ID to which the Otpion is associated as GUID
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Name of the Option
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the option
        /// </summary>
        public string Description { get; set; }
         
    }
}
