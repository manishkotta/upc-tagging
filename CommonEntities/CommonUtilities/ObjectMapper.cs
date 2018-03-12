using Business.Entities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProvider
{
    public static class ObjectMapper
    {

        public static List<UntaggedUPCBusinessModal> CreateMap(List<UntaggedUPC> repoObject)
        {
            return repoObject.Select(s => new UntaggedUPCBusinessModal()
            {
                UPCCode = s.UPCCode,
                UntaggedUPCID = s.UntaggedUPCID,
                Description = s.Description,
                DescriptionID = s.DescriptionID,
                ItemAssignedBy = s.ItemAssignedBy,
                ItemAssignedTo = s.ItemAssignedTo,
                ProductType = ObjectMapper.CreateMap(s.ProductType),
                ProductCategory = ObjectMapper.CreateMap(s.ProductCategory),
                ProductSubCategory = ObjectMapper.CreateMap(s.ProductSubCategory),
                ProductSizing = s.ProductSizing
            }).ToList();
        }

        public static List<Business.Entities.TaggedUPC> CreateMap(List<Repositories.Entities.TaggedUPC> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.TaggedUPC()
            {
                UPCCode = s.UPCCode

            }).ToList();
        }

        public static Business.Entities.UntaggedUPCBusinessModal CreateMap(UntaggedUPC repoObject)
        {
            return new UntaggedUPCBusinessModal()
            {
                UPCCode = repoObject.UPCCode,
                UntaggedUPCID = repoObject.UntaggedUPCID,
                Description = repoObject.Description,
                DescriptionID = repoObject.DescriptionID,
                ItemAssignedBy = repoObject.ItemAssignedBy,
                ItemAssignedTo = repoObject.ItemAssignedTo,
                ProductType = ObjectMapper.CreateMap(repoObject.ProductType),
                ProductCategory = ObjectMapper.CreateMap(repoObject.ProductCategory),
                ProductSubCategory = ObjectMapper.CreateMap(repoObject.ProductSubCategory),
                ProductSizing = repoObject.ProductSizing,
                StatusID = repoObject.StatusID
            };
        }

        public static UntaggedUPC CreateMap(Business.Entities.UntaggedUPCBusinessModal businessObject)
        {
            return new UntaggedUPC()
            {
                UPCCode = businessObject.UPCCode,
                UntaggedUPCID = businessObject.UntaggedUPCID,
                Description = businessObject.Description,
                DescriptionID = businessObject.DescriptionID,
                ItemAssignedBy = businessObject.ItemAssignedBy,
                ItemAssignedTo = businessObject.ItemAssignedTo,
                ProductType = ObjectMapper.CreateMap(businessObject.ProductType),
                ProductCategory = ObjectMapper.CreateMap(businessObject.ProductCategory),
                ProductSubCategory = ObjectMapper.CreateMap(businessObject.ProductSubCategory),
                ProductSizing = businessObject.ProductSizing,
                StatusID = businessObject.StatusID.HasValue ? businessObject.StatusID.Value : default(int?)
            };
        }


        public static List<Business.Entities.ProductType> CreateMap(List<Repositories.Entities.ProductType> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductType()
            {
                ProductTypeName = s.ProductTypeName,
                TypeID = s.TypeID

            }).ToList();
        }

        public static List<Business.Entities.ProductCategory> CreateMap(List<Repositories.Entities.ProductCategory> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductCategory()
            {
                CategoryName = s.CategoryName,
                CategoryID = s.CategoryID

            }).ToList();
        }

        public static List<Business.Entities.ProductSubCategory> CreateMap(List<Repositories.Entities.ProductSubCategory> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.ProductSubCategory()
            {
                SubCategoryID = s.SubCategoryID,
                SubcategoryName = s.SubcategoryName

            }).ToList();
        }

        public static Business.Entities.ProductType CreateMap(Repositories.Entities.ProductType productType)
        {
            if (productType == null) return null;
            return new Business.Entities.ProductType { ProductTypeName = productType.ProductTypeName, TypeID = productType.TypeID };
        }

        public static Business.Entities.ProductCategory CreateMap(Repositories.Entities.ProductCategory productCategory)
        {
            if (productCategory == null) return null;
            return new Business.Entities.ProductCategory { CategoryName = productCategory.CategoryName, CategoryID = productCategory.CategoryID };
        }

        public static Business.Entities.ProductSubCategory CreateMap(Repositories.Entities.ProductSubCategory productSubCategory)
        {
            if (productSubCategory == null) return null;
            return new Business.Entities.ProductSubCategory { SubcategoryName = productSubCategory.SubcategoryName, SubCategoryID = productSubCategory.SubCategoryID };
        }

        public static Repositories.Entities.ProductType CreateMap(Business.Entities.ProductType productType)
        {
            if (productType == null) return null;
            return new Repositories.Entities.ProductType { ProductTypeName = productType.ProductTypeName, TypeID = productType.TypeID };
        }

        public static Repositories.Entities.ProductCategory CreateMap(Business.Entities.ProductCategory productCategory)
        {
            if (productCategory == null) return null;
            return new Repositories.Entities.ProductCategory { CategoryName = productCategory.CategoryName, CategoryID = productCategory.CategoryID };
        }

        public static Repositories.Entities.ProductSubCategory CreateMap(Business.Entities.ProductSubCategory productSubCategory)
        {
            if (productSubCategory == null) return null;
            return new Repositories.Entities.ProductSubCategory { SubcategoryName = productSubCategory.SubcategoryName, SubCategoryID = productSubCategory.SubCategoryID };
        }


        public static List<Business.Entities.User> CreateMap(List<Repositories.Entities.User> userRepoObj)
        {
            return userRepoObj.Select(s =>
                new Business.Entities.User
                {
                    Email = s.Email,
                    Name = s.Name,
                    RoleID = s.RoleID,
                    UserID = s.UserID,
                    UserName = s.UserName
                }).ToList();
        }


    }
}
