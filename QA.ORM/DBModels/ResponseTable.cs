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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponseTable()
        {
            Respondent_answer = new HashSet<Respondent_answer>();
        }

        [Key]
        public int RID { get; set; }

        public int QAID { get; set; }

        public DateTime AnswerDate { get; set; }

        public Guid RespondentID { get; set; }

        public virtual QAInfo QAInfo { get; set; }

        public virtual RespondentInfo RespondentInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respondent_answer> Respondent_answer { get; set; }
    }
}
