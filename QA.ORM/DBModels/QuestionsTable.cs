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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionsTable()
        {
            QA_Question = new HashSet<QA_Question>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QA_Question> QA_Question { get; set; }
    }
}
