using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class BookmarkMap : EntityTypeConfiguration<Bookmark>
	{
		public BookmarkMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.TargetUrl)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.Remark)
				.HasMaxLength(500);
				
			// Table & Column Mappings
			this.ToTable("Bookmarks");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.CreatorID).HasColumnName("CreatorID");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.TargetUrl).HasColumnName("TargetUrl");
			this.Property(t => t.Remark).HasColumnName("Remark");
			this.Property(t => t.InsertTime).HasColumnName("InsertTime");
			this.Property(t => t.IsPrivate).HasColumnName("IsPrivate");
			this.Property(t => t.UpCount).HasColumnName("UpCount");
			this.Property(t => t.State).HasColumnName("State");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.Bookmarks)
				.HasForeignKey(d => d.CreatorID);
				
		}
	}
}

