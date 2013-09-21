using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class EventLogMap : EntityTypeConfiguration<EventLog>
	{
		public EventLogMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ClientIP)
				.HasMaxLength(50);
				
			this.Property(t => t.EventTitle)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.EventSummary)
				.IsRequired();
				
			// Table & Column Mappings
			this.ToTable("EventLogs");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.UserID).HasColumnName("UserID");
			this.Property(t => t.OccurTime).HasColumnName("OccurTime");
			this.Property(t => t.ClientIP).HasColumnName("ClientIP");
			this.Property(t => t.EventType).HasColumnName("EventType");
			this.Property(t => t.EventTitle).HasColumnName("EventTitle");
			this.Property(t => t.EventSummary).HasColumnName("EventSummary");
		}
	}
}

