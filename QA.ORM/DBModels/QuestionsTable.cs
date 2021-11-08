namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionsTable")]
    public partial class QuestionsTable
    {
        [Key]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionTitle { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; }

        public int? ChoiceID { get; set; }

        public bool? CommonQuestion { get; set; }
    }
}
