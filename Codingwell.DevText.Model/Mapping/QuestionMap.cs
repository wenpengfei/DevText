using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class QuestionMap : EntityTypeConfiguration<Question>
	{
		public QuestionMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Path)
				.HasMaxLength(200);
				
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.Summary)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.Content)
				.IsRequired();
				
			// Table & Column Mappings
			this.ToTable("Questions");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.AskerID).HasColumnName("AskerID");
			this.Property(t => t.Path).HasColumnName("Path");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.Summary).HasColumnName("Summary");
			this.Property(t => t.Content).HasColumnName("Content");
			this.Property(t => t.OccurTime).HasColumnName("OccurTime");
			this.Property(t => t.State).HasColumnName("State");
			this.Property(t => t.DownCount).HasColumnName("DownCount");
			this.Property(t => t.UpCount).HasColumnName("UpCount");
			this.Property(t => t.FavouriteCount).HasColumnName("FavouriteCount");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.Questions)
				.HasForeignKey(d => d.AskerID);
				
		}
	}
}

