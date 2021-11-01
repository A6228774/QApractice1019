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
        public virtual DbSet<CustomizeQA> CustomizeQA { get; set; }
        public virtual DbSet<QuestionsTable> QuestionsTable { get; set; }
        public virtual DbSet<RespondentInfo> RespondentInfo { get; set; }
        public virtual DbSet<QADesign> QADesign { get; set; }
        public virtual DbSet<Respondent_answer> Respondent_answer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionsTable>()
                .Property(e => e.QuestionTitle)
                .IsFixedLength();

            modelBuilder.Entity<QuestionsTable>()
                .Property(e => e.QuestionType)
                .IsFixedLength();

            modelBuilder.Entity<QuestionsTable>()
                .HasMany(e => e.QADesign)
                .WithRequired(e => e.QuestionsTable)
                .WillCascadeOnDelete(false);
        }
    }
}