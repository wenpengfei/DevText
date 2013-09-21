using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class QuestionAnswerMap : EntityTypeConfiguration<QuestionAnswer>
	{
		public QuestionAnswerMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
				
			this.Property(t => t.Content)
				.IsRequired();
				
			// Table & Column Mappings
			this.ToTable("QuestionAnswers");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.QuestionID).HasColumnName("QuestionID");
			this.Property(t => t.AnswerID).HasColumnName("AnswerID");
			this.Property(t => t.Content).HasColumnName("Content");
			this.Property(t => t.AnswerTime).HasColumnName("AnswerTime");
			this.Property(t => t.Useful).HasColumnName("Useful");
			this.Property(t => t.UnUseful).HasColumnName("UnUseful");
			this.Property(t => t.CommentsCount).HasColumnName("CommentsCount");

			// Relationships
			this.HasRequired(t => t.Question)
				.WithMany(t => t.QuestionAnswers)
				.HasForeignKey(d => d.QuestionID);
				
			this.HasRequired(t => t.User)
				.WithMany(t => t.QuestionAnswers)
				.HasForeignKey(d => d.AnswerID);
				
		}
	}
}

