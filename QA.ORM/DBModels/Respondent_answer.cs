namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Respondent_answer
    {
        [Key]
        [Column(Order = 0)]
        public Guid RespondentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QAID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        public int? ChoiceID { get; set; }

        [StringLength(100)]
        public string Answer { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RID { get; set; }

        public virtual QAInfo QAInfo { get; set; }

        public virtual RespondentInfo RespondentInfo { get; set; }

        public virtual ResponseTable ResponseTable { get; set; }
    }
}
