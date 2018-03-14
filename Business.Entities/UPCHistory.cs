using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class UPCHistory
    {
        public long UPCHistoryID { get; set; }

        public string TaggedUPCCode { get; set; }

        public int? SubmittedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime ItemInsertedAt { get; set; }

        public DateTime ItemModifiedAt { get; set; }

        public int? ModifiedSubmittedBy { get; set; }

        public int? ModifiedApprovedBy { get; set; }
    }
}
