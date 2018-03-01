using Business.Entities;
using CommonEntities;
using IBusiness;
using Repositories.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProvider
{
    public class ObjectMapper :IObjectMapper
    {

        public List<UntaggedUPCBusinessModal> GroupMapper(List<UntaggedUPC> repoObject)
        {
            return repoObject.Select(s => new UntaggedUPCBusinessModal()
            {
                UPCCode = s.UPCCode,
                UntaggedUPCID = s.UntaggedUPCID,
                Description = s.Description,
                DescriptionID = s.DescriptionID,
                ItemAssignedBy = s.ItemAssignedBy,
                ItemAssignedTo = s.ItemAssignedTo,
                ProductType = s.ProductType?.ProductTypeName,
                ProductCategory = s.ProductCategory?.Category,
                ProductSubCategory = s.ProductSubCategory?.SubcategoryName,
                ProductSizing = s.ProductSizing
            }).ToList();
        }

        public List<Business.Entities.TaggedUPC> GroupMapper(List<Repositories.Entities.TaggedUPC> repoObject)
        {
            return repoObject.Select(s => new Business.Entities.TaggedUPC()
            {
                UPCCode = s.UPCCode

            }).ToList();
        }

    }
}
