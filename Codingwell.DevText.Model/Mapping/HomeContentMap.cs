using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class HomeContentMap : EntityTypeConfiguration<HomeContent>
	{
		public HomeContentMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.LinkUrl)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.ContentData)
				.HasMaxLength(1000);
				
			// Table & Column Mappings
			this.ToTable("HomeContents");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ContentType).HasColumnName("ContentType");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.LinkUrl).HasColumnName("LinkUrl");
			this.Property(t => t.ContentData).HasColumnName("ContentData");
			this.Property(t => t.InsertTime).HasColumnName("InsertTime");
		}
	}
}

