using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public  class SurveyContext: DbContext
    {
        private readonly IConfiguration _config; 
        public SurveyContext(IConfiguration configuration) { _config = configuration; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("Connection"));
            }
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SurveyXQuestion> SurveysXQuestions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
            entity => {
                entity.ToTable("User");
                entity.HasIndex(d => d.UserName)
                    .HasName("Uq_User_UserName")
                    .IsUnique();
            });

            modelBuilder.Entity<Survey>(
            entity => {
                entity.ToTable("Survey");
                entity.HasIndex(d => d.Name)
                      .HasName("Uq_Survey_Name")
                      .IsUnique();
            });

            modelBuilder.Entity<Question>(
            entity => {
                entity.ToTable("Question");
                entity.HasIndex(d => d.Name)
                    .HasName("Uq_Question_Name")
                    .IsUnique();
                entity.HasIndex(d => d.Title)
                    .HasName("Uq_Question_Title")
                    .IsUnique();
            });


            modelBuilder.Entity<SurveyXQuestion>(
            entity => {
                entity.ToTable("SurveyXQuestion");
                entity.HasIndex(d => new { d.SurveyId , d.QuestionId})
                      .HasName("Uq_SurveyXQuestion_SurveyXQuestion")
                      .IsUnique();
                entity.HasOne(d => d.Survey)
                      .WithMany(d => d.SurveysXQuestions)
                      .HasForeignKey(d => d.SurveyId)
                      .HasConstraintName("fk_SurveyXQuestion_Survey");
                entity.HasOne(d => d.Question)
                      .WithMany(d => d.SurveysXQuestions)
                      .HasForeignKey(d => d.QuestionId)
                      .HasConstraintName("fk_SurveyXQuestion_Question");
            });

            modelBuilder.Entity<Document>(
            entity => {
                entity.ToTable("Document");
                entity.HasIndex(d => d.Code)
                    .HasName("Uq_Document_Code")
                    .IsUnique();
            });

            modelBuilder.Entity<Answer>(
            entity => {
                entity.ToTable("Answer");
                entity.HasOne(d => d.SurveyXQuestion)
                      .WithMany(d => d.Answers)
                      .HasForeignKey(d => d.SurveyXQuestionId)
                      .HasConstraintName("fk_Answer_SurveyXQuestion");
                entity.HasOne(d => d.Document)
                      .WithMany(d => d.Answers)
                      .HasForeignKey(d => d.DocumentId)
                      .HasConstraintName("fk_Answer_Document");
            });
        }
    }
}
