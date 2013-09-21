using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping {
	public class AnswerMap : EntityTypeConfiguration<Answer> {
		public AnswerMap ( ) {
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Content)
				.IsRequired();

			// Table & Column Mappings
			this.ToTable("Answers");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.QuestionID).HasColumnName("QuestionID");
			this.Property(t => t.AnswerID).HasColumnName("AnswerID");
			this.Property(t => t.Content).HasColumnName("Content");
			this.Property(t => t.OccurTime).HasColumnName("OccurTime");
			this.Property(t => t.State).HasColumnName("State");
			this.Property(t => t.UpCount).HasColumnName("UpCount");
			this.Property(t => t.DownCount).HasColumnName("DownCount");

			// Relationships
			this.HasRequired(t => t.Question)
				.WithMany(t => t.Answers)
				.HasForeignKey(d => d.QuestionID);

			this.HasRequired(t => t.User)
				.WithMany(t => t.Answers)
				.HasForeignKey(d => d.AnswerID);

		}
	}
}

