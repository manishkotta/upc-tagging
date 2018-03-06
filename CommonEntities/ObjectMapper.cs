using Business.Entities;
using Repositories.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProvider
{
    public static class ObjectMapper 
    {

        public static List<UntaggedUPCBusinessModal> GroupMapper(List<UntaggedUPC> repoObject)
        {
            return repoObject.Select(s => new UntaggedUPCBusinessModal()
            {
                UPCCode = s.UPCCode,
                UntaggedUPCID = s.UntaggedUPCID,
                Description = s.Description,
                DescriptionID = s.DescriptionID,
                ItemAssignedBy = s.ItemAssignedBy,
                ItemAssignedTo = s.ItemAssignedTo,
                ProductType = ObjectMapper.ObjectToObjectMapper(s.ProductType),
                ProductCategory = ObjectMapper.ObjectToObjectMapper(s.ProductCategory),
                ProductSubCategory = ObjectMapper.ObjectToObjectMapper(s.ProductSubCategory),
                ProductSizing = s.ProductSizing
            }).ToList();
        }

        public static List<Business.Entities.TaggedUPC> GroupMapper(List<Repositories.Entities.TaggedUPC> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.TaggedUPC()
            {
                UPCCode = s.UPCCode

            }).ToList();
        }

        public static List<Business.Entities.ProductType> GroupMapper(List<Repositories.Entities.ProductType> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductType()
            {
                 ProductTypeName = s.ProductTypeName,
                 TypeID = s.TypeID

            }).ToList();
        }

        public static List<Business.Entities.ProductCategory> GroupMapper(List<Repositories.Entities.ProductCategory> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductCategory()
            {
                CategoryName = s.CategoryName,
                CategoryID = s.CategoryID

            }).ToList();
        }

        public static List<Business.Entities.ProductSubCategory> GroupMapper(List<Repositories.Entities.ProductSubCategory> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductSubCategory()
            {
                SubCategoryID = s.SubCategoryID,
                SubcategoryName = s.SubcategoryName

            }).ToList();
        }

        public static Business.Entities.ProductType ObjectToObjectMapper(Repositories.Entities.ProductType productType)
        {
            if (productType == null) return null;
            return new Business.Entities.ProductType { ProductTypeName = productType.ProductTypeName, TypeID = productType.TypeID };
        }

        public static Business.Entities.ProductCategory ObjectToObjectMapper(Repositories.Entities.ProductCategory productCategory)
        {
            if (productCategory == null) return null;
            return new Business.Entities.ProductCategory { CategoryName = productCategory.CategoryName, CategoryID = productCategory.CategoryID };
        }

        public static Business.Entities.ProductSubCategory ObjectToObjectMapper(Repositories.Entities.ProductSubCategory productSubCategory)
        {
            if (productSubCategory == null) return null;
            return new Business.Entities.ProductSubCategory { SubcategoryName = productSubCategory.SubcategoryName, SubCategoryID = productSubCategory.SubCategoryID };
        }
    }
}
