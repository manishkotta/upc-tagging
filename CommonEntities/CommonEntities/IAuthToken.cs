using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonEntities
{
    public interface IAuthToken
    {
        DateTime ValidTo { get; }
        string Value { get; }
    }
}
