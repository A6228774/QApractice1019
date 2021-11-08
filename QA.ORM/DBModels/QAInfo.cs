namespace QA.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QAInfo")]
    public partial class QAInfo
    {
        [Key]
        public int QAID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Summary { get; set; }

        public bool IsEnabled { get; set; }
    }
}
