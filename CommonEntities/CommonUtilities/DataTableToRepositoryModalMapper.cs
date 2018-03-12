using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.CommonUtilities
{
    public static class DataTableToRepositoryModalMapper
    {
        public static List<UntaggedUPC> DataTableToUntaggedUPCGroup(this DataTable dt)
        {
            List<UntaggedUPC> untaggedUPCGroup = new List<UntaggedUPC>();
            foreach (DataRow dr in dt.Rows)
            {
               var untaggedUPC = new UntaggedUPC()
                {
                    UntaggedUPCID = Convert.ToInt32(dr["untaggedupcid"]),
                    Description = dr["description"] == DBNull.Value ? string.Empty : Convert.ToString(dr["description"]),
                    UPCCode = dr["upccode"] ==DBNull.Value ? string.Empty : Convert.ToString(dr["upccode"]),
                    DescriptionID = dr["descriptionid"] == DBNull.Value ? default(int) : Convert.ToInt32(dr["descriptionid"]),
                    ProductCategory = dr["categoryid"] == DBNull.Value ? null : new ProductCategory
                    {
                        CategoryID = Convert.ToInt32(dr["categoryid"]),
                        CategoryName = Convert.ToString(dr["category"])
                    },
                    ProductType = dr["typeid"] == DBNull.Value ? null : new ProductType
                    {
                        ProductTypeName = Convert.ToString(dr["producttype"]),
                        TypeID = Convert.ToInt32(dr["typeid"])
                    },
                    ProductSubCategory = dr["subcategoryid"] == DBNull.Value ? null : new ProductSubCategory
                    {
                        SubcategoryName = Convert.ToString(dr["subcategory"]),
                        SubCategoryID = Convert.ToInt32(dr["subcategoryid"])
                    },
                    ItemAssignedTo = dr["itemassingedto"] == DBNull.Value ? default(int?) : Convert.ToInt32(dr["itemassingedto"]),
                    ProductSizing = dr["productsizing"] == DBNull.Value ? string.Empty : Convert.ToString(dr["productsizing"])
                };
                untaggedUPCGroup.Add(untaggedUPC);
            }
            return untaggedUPCGroup;
        }

        public static List<TaggedUPC> DataTableToTaggedUPCGroup(this DataTable dt)
        {
            List<TaggedUPC> taggedUPCGroup = new List<TaggedUPC>();
            foreach (DataRow dr in dt.Rows)
            {
                var taggedUPC = new TaggedUPC()
                {
                    Description = dr["description"] == DBNull.Value ? string.Empty : Convert.ToString(dr["description"]),
                    UPCCode = dr["upccode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["upccode"]),
                    DescriptionID = dr["descriptionid"] == DBNull.Value ? default(int) : Convert.ToInt32(dr["descriptionid"]),
                    ProductCategory = dr["categoryid"] == DBNull.Value ? null : new ProductCategory
                    {
                        CategoryID = Convert.ToInt32(dr["categoryid"]),
                        CategoryName = Convert.ToString(dr["category"])
                    },
                    ProductType = dr["typeid"] == DBNull.Value ? null : new ProductType
                    {
                        ProductTypeName = Convert.ToString(dr["producttype"]),
                        TypeID = Convert.ToInt32(dr["typeid"])
                    },
                    ProductSubCategory = dr["subcategoryid"] == DBNull.Value ? null : new ProductSubCategory
                    {
                        SubcategoryName = Convert.ToString(dr["subcategory"]),
                        SubCategoryID = Convert.ToInt32(dr["subcategoryid"])
                    },
                    ProductSizing = dr["productsizing"] == DBNull.Value ? string.Empty : Convert.ToString(dr["productsizing"])
                };
                taggedUPCGroup.Add(taggedUPC);
            }
            return taggedUPCGroup;
        }


    }
}
