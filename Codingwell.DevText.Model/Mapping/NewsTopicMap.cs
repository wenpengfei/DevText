using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class NewsTopicMap : EntityTypeConfiguration<NewsTopic>
	{
		public NewsTopicMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.FilePath)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.SortKey)
				.IsRequired()
				.HasMaxLength(5);
				
			// Table & Column Mappings
			this.ToTable("NewsTopic");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.FilePath).HasColumnName("FilePath");
			this.Property(t => t.SortKey).HasColumnName("SortKey");
			this.Property(t => t.State).HasColumnName("State");
		}
	}
}

