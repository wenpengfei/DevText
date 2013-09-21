using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class SysLogMap : EntityTypeConfiguration<SysLog>
	{
		public SysLogMap()
		{
			// Primary Key
			this.HasKey(t => t.LogID);

			// Properties
			this.Property(t => t.LogThread)
				.HasMaxLength(500);
				
			this.Property(t => t.LogLevel)
				.HasMaxLength(50);
				
			this.Property(t => t.Logger)
				.HasMaxLength(500);
				
			// Table & Column Mappings
			this.ToTable("SysLogs");
			this.Property(t => t.LogID).HasColumnName("LogID");
			this.Property(t => t.LogDate).HasColumnName("LogDate");
			this.Property(t => t.LogThread).HasColumnName("LogThread");
			this.Property(t => t.LogLevel).HasColumnName("LogLevel");
			this.Property(t => t.Logger).HasColumnName("Logger");
			this.Property(t => t.LogMessage).HasColumnName("LogMessage");
			this.Property(t => t.LogException).HasColumnName("LogException");
		}
	}
}

