using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class UPCHistory
    {
        public long UPCHistoryID { get; set; }

        [ForeignKey("taggedupccode")]
        public string TaggedUPCCode { get; set; }

        public int? SubmittedBy { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime ItemInsertedAt { get; set; }

        public DateTime ItemModifiedAt { get; set; }

        public int? ModifiedSubmittedBy { get; set; }

        public int? ModifiedApprovedBy { get; set; }
        public virtual TaggedUPC TaggedUPC { get; set; }
    }
}
