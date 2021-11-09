namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QA_Question_View
    {
        [Key]
        [Column(Order = 0)]
        public bool MustKey { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string QuestionType { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string QuestionTitle { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QAID { get; set; }
    }
}
