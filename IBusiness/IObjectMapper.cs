using System;
using System.Collections.Generic;
using System.Text;

namespace IBusiness
{
    public interface IObjectMapper
    {
        List<Business.Entities.UntaggedUPCBusinessModal> GroupMapper(List<Repositories.Entities.UntaggedUPC> repoObject);

        List<Business.Entities.TaggedUPC> GroupMapper(List<Repositories.Entities.TaggedUPC> repoObject);
    }
}
