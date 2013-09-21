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
	public class QuestionSupplementMap : EntityTypeConfiguration<QuestionSupplement>
	{
		public QuestionSupplementMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
				
			this.Property(t => t.Content)
				.IsRequired()
				.HasMaxLength(300);
				
			// Table & Column Mappings
			this.ToTable("QuestionSupplements");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.QuestionID).HasColumnName("QuestionID");
			this.Property(t => t.AddUserID).HasColumnName("AddUserID");
			this.Property(t => t.Content).HasColumnName("Content");
			this.Property(t => t.AddTime).HasColumnName("AddTime");

			// Relationships
			this.HasRequired(t => t.Question)
				.WithMany(t => t.QuestionSupplements)
				.HasForeignKey(d => d.QuestionID);
				
			this.HasRequired(t => t.User)
				.WithMany(t => t.QuestionSupplements)
				.HasForeignKey(d => d.AddUserID);
				
		}
	}
}

