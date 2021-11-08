namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseTable")]
    public partial class ResponseTable
    {
        [Key]
        public int ResponseID { get; set; }

        public int QAID { get; set; }

        public DateTime AnswerDate { get; set; }

        public Guid RespondentID { get; set; }

        public virtual RespondentInfo RespondentInfo { get; set; }
    }
}
