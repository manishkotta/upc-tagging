﻿using Common.CommonEntities;
using Common.CommonUtilities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUntaggedUPCRepository
    {
        Task<Result<List<UntaggedUPC>>> GetUntaggedUPCList(UPCSearchFilter upcSearch);

        Task<Result<UntaggedUPC>> UpdateUntaggedUPC(UntaggedUPC upc);

        Task<Result> AssignUserToUntaggedUPC(int[] untaggedUPCIDs, User user, int adminUserID);

        Task<Result<UntaggedUPC>> GetUntaggedUPCOnID(int untaggedUPCID);

        Task<Result> Delete(int untaggedUPCID);
    }
}
