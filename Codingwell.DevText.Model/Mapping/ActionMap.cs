using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class ActionMap : EntityTypeConfiguration<Action>
	{
		public ActionMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.ActionName)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.ActionValue)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.PageUrl)
				.HasMaxLength(200);
				
			// Table & Column Mappings
			this.ToTable("Actions");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ChannelID).HasColumnName("ChannelID");
			this.Property(t => t.ActionName).HasColumnName("ActionName");
			this.Property(t => t.ActionValue).HasColumnName("ActionValue");
			this.Property(t => t.PageUrl).HasColumnName("PageUrl");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.State).HasColumnName("State");

			// Relationships
			this.HasMany(t => t.Roles)
				.WithMany(t => t.Actions)
				.Map(m =>
					{
						m.ToTable("RoleActions");
						m.MapLeftKey("ActionID");
						m.MapRightKey("RoleID");
					});
					
			this.HasRequired(t => t.ActionChannel)
				.WithMany(t => t.Actions)
				.HasForeignKey(d => d.ChannelID);
				
		}
	}
}

