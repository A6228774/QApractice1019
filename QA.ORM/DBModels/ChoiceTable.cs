namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChoiceTable")]
    public partial class ChoiceTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChoiceTable()
        {
            Respondent_answer = new HashSet<Respondent_answer>();
        }

        [Key]
        public int ChoiceID { get; set; }

        [StringLength(20)]
        public string FirstChoice { get; set; }

        [StringLength(20)]
        public string SecondChoice { get; set; }

        [StringLength(20)]
        public string ThirdChoice { get; set; }

        [StringLength(20)]
        public string ForthChoice { get; set; }

        [StringLength(20)]
        public string FifthChoice { get; set; }

        [StringLength(20)]
        public string SixthChoice { get; set; }

        public int ChoiceCount { get; set; }

        public virtual ChoiceTable ChoiceTable1 { get; set; }

        public virtual ChoiceTable ChoiceTable2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respondent_answer> Respondent_answer { get; set; }
    }
}
