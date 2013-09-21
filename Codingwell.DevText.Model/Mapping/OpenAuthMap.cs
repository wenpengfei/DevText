using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping {
	public class OpenAuthMap : EntityTypeConfiguration<OpenAuth> {
		public OpenAuthMap ( ) {
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.OpenId)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.AccessTokenKey)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.AccessTokenSecret)
				.IsRequired()
				.HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("OpenAuth");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.UserID).HasColumnName("UserID");
			this.Property(t => t.OpenId).HasColumnName("OpenId");
			this.Property(t => t.AccessTokenKey).HasColumnName("AccessTokenKey");
			this.Property(t => t.AccessTokenSecret).HasColumnName("AccessTokenSecret");
			this.Property(t => t.InsertTime).HasColumnName("InsertTime");
			this.Property(t => t.OpenAuthType).HasColumnName("OpenAuthType");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.OpenAuths)
				.HasForeignKey(d => d.UserID);

		}
	}
}

