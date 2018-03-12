using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.CommonUtilities
{
    public static class Constants
    {
        public const string PostgresqlConnStr = "PostgresqlConnectionString";

        public const string BadRequestErrorMessage = "Oops... Something Went Wrong";

        public const string UnTaggedUPCQuery = @"SELECT s.untaggedupcid, s.description, s.descriptionid, s.itemassignedby, s.itemassingedto, s.iteminsertedat, s.iteminsertedby, s.itemmodifiedat, s.itemmodifiedby, s.productsizing, s.statusid, s.upccode, s.productcategoryid, s.productsubcategoryid, s.producttypeid, pst.subcategoryid, pst.subcategory, pc.categoryid, pc.category, pt.typeid, pt.producttype
                                                 FROM untaggedupc AS s
                                                 LEFT JOIN subcategory AS pst ON s.productsubcategoryid = pst.subcategoryid
                                                 LEFT JOIN category AS pc ON s.productcategoryid = pc.categoryid
                                                 LEFT JOIN producttype AS pt ON s.producttypeid = pt.typeid";

        public const string TaggedUPCQuery = @"SELECT s.description, s.descriptionid, s.productsizing, s.upccode, s.productcategoryid, s.productsubcategoryid, s.producttypeid, pst.subcategoryid, pst.subcategory, pc.categoryid, pc.category, pt.typeid, pt.producttype
                                                 FROM taggedupc AS s
                                                 LEFT JOIN subcategory AS pst ON s.productsubcategoryid = pst.subcategoryid
                                                 LEFT JOIN category AS pc ON s.productcategoryid = pc.categoryid
                                                 LEFT JOIN producttype AS pt ON s.producttypeid = pt.typeid";


        public const string No_Records_Found = "No records found";

    }
}
