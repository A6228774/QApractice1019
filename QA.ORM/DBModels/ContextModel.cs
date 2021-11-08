using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QA.ORM.DBModels
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<ChoiceTable> ChoiceTable { get; set; }
        public virtual DbSet<QAInfo> QAInfo { get; set; }
        public virtual DbSet<QuestionsTable> QuestionsTable { get; set; }
        public virtual DbSet<RespondentInfo> RespondentInfo { get; set; }
        public virtual DbSet<ResponseTable> ResponseTable { get; set; }
        public virtual DbSet<QA_Question> QA_Question { get; set; }
        public virtual DbSet<Respondent_answer> Respondent_answer { get; set; }
        public virtual DbSet<CSVOutput_View> CSVOutput_View { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RespondentInfo>()
                .HasMany(e => e.ResponseTable)
                .WithRequired(e => e.RespondentInfo)
                .WillCascadeOnDelete(false);
        }
    }
}
