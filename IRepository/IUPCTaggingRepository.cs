using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IUPCTaggingRepository
    {
        bool ExtractUntaggedUPCs();
    }
}
