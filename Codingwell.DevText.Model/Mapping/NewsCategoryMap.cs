using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class NewsCategoryMap : EntityTypeConfiguration<NewsCategory>
	{
		public NewsCategoryMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.ImgPath)
				.HasMaxLength(200);
				
			// Table & Column Mappings
			this.ToTable("NewsCategories");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ParentID).HasColumnName("ParentID");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.ImgPath).HasColumnName("ImgPath");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.LastChanged).HasColumnName("LastChanged");
			this.Property(t => t.State).HasColumnName("State");
		}
	}
}

