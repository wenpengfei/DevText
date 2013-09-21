using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class BuildLogMap : EntityTypeConfiguration<BuildLog>
	{
		public BuildLogMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.LogContent)
				.IsRequired()
				.HasMaxLength(1000);
				
			// Table & Column Mappings
			this.ToTable("BuildLogs");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.LogContent).HasColumnName("LogContent");
			this.Property(t => t.InsertTime).HasColumnName("InsertTime");
			this.Property(t => t.RankIndex).HasColumnName("RankIndex");
			this.Property(t => t.State).HasColumnName("State");
		}
	}
}

