using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class ProductType
    {
        [Column("typeid")]
        public int TypeID { get; set; }
        [Column("producttypename")]
        public string ProductTypeName { get; set; }
    }
}
