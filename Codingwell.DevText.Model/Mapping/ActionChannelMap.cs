using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class ActionChannelMap : EntityTypeConfiguration<ActionChannel>
	{
		public ActionChannelMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("ActionChannels");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.Rank).HasColumnName("Rank");
			this.Property(t => t.State).HasColumnName("State");
		}
	}
}

