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
    }
}
