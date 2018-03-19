using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class TaggedUPC
    {
        [Column("descriptionid")]
        public long DescriptionID { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("upccode")]
        public string UPCCode { get; set; }
        [Column("producttypeid")]
        public int? ProductTypeID { get; set; }

        [Column("productcategoryid")]
        public int? ProductCategoryID { get; set; }
        [Column("productsubcategoryid")]
        public int? ProductSubcategoryID { get; set; }

        [Column("productsizing")]
        public string ProductSizing { get; set; }

        //[Column("ismigrated")]
        //public bool IsMigrated { get; set; }

        //[ForeignKey("producttypeid")]
        public virtual ProductType ProductType { get; set; }

        //[ForeignKey("productcategoryid")]
        public virtual ProductCategory ProductCategory { get; set; }

        //[ForeignKey("productsubcategoryid")]
        public virtual ProductSubCategory ProductSubCategory { get; set; }

        //public virtual UPCHistory UPCHistroy { get; set; }

    }
}
