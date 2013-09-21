using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class CommentMap : EntityTypeConfiguration<Comment>
	{
		public CommentMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ModuleType)
				.IsRequired()
				.HasMaxLength(20);
				
			this.Property(t => t.CommentContent)
				.IsRequired()
				.HasMaxLength(800);
				
			// Table & Column Mappings
			this.ToTable("Comments");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ModuleType).HasColumnName("ModuleType");
			this.Property(t => t.CreatorID).HasColumnName("CreatorID");
			this.Property(t => t.CommentContent).HasColumnName("CommentContent");
			this.Property(t => t.UpCount).HasColumnName("UpCount");
			this.Property(t => t.DownCount).HasColumnName("DownCount");
			this.Property(t => t.ReportCount).HasColumnName("ReportCount");
			this.Property(t => t.InsertTime).HasColumnName("InsertTime");
			this.Property(t => t.State).HasColumnName("State");

			// Relationships
			this.HasMany(t => t.News)
			    .WithMany(t => t.Comments)
				.Map(m =>
                    {
                        m.ToTable("NewsCommentsRelation");
                        m.MapLeftKey("CommentID");
                        m.MapRightKey("NewsID");
                    });
					
		}
	}
}

