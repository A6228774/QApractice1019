namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CSVOutput_View
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid RespondentID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime AnswerDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QAID { get; set; }
    }
}
