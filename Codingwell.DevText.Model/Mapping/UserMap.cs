using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			// Primary Key
			this.HasKey(t => t.UserID);

			// Properties
			this.Property(t => t.NickName)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.Figureurl)
				.HasMaxLength(500);
				
			this.Property(t => t.Figureurl50)
				.HasMaxLength(500);
				
			this.Property(t => t.Figureurl100)
				.HasMaxLength(500);
				
			this.Property(t => t.Email)
				.HasMaxLength(50);
				
			this.Property(t => t.MSN)
				.HasMaxLength(50);
				
			this.Property(t => t.LastLoginIP)
				.HasMaxLength(50);
				
			this.Property(t => t.ActiveCode)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("Users");
			this.Property(t => t.UserID).HasColumnName("UserID");
			this.Property(t => t.NickName).HasColumnName("NickName");
			this.Property(t => t.Figureurl).HasColumnName("Figureurl");
			this.Property(t => t.Figureurl50).HasColumnName("Figureurl50");
			this.Property(t => t.Figureurl100).HasColumnName("Figureurl100");
			this.Property(t => t.Email).HasColumnName("Email");
			this.Property(t => t.MSN).HasColumnName("MSN");
			this.Property(t => t.QQ).HasColumnName("QQ");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.LastLoginTime).HasColumnName("LastLoginTime");
			this.Property(t => t.LastLoginIP).HasColumnName("LastLoginIP");
			this.Property(t => t.OpenAuthType).HasColumnName("OpenAuthType");
			this.Property(t => t.AccountState).HasColumnName("AccountState");
			this.Property(t => t.ActiveCode).HasColumnName("ActiveCode");
		}
	}
}

