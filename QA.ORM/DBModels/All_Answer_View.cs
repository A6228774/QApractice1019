namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class All_Answer_View
    {
        [Key]
        [Column(Order = 0)]
        public DateTime AnswerDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RID { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid RespondentID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QAID { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        public int? ChoiceID { get; set; }

        [StringLength(100)]
        public string Answer { get; set; }
    }
}
