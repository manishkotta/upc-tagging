using Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class AssignUntagUpcDTO
    {
        public int[] untaggedUPCIDs { get; set; }
        public User user { get; set; }
    }
}
