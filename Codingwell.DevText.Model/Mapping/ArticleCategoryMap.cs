using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping {
	public class ArticleCategoryMap : EntityTypeConfiguration<ArticleCategory> {
		public ArticleCategoryMap ( ) {
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.ImgPath)
				.HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("ArticleCategories");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.OwnerID).HasColumnName("OwnerID");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.ImgPath).HasColumnName("ImgPath");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.LastChanged).HasColumnName("LastChanged");
			this.Property(t => t.State).HasColumnName("State");

			// Relationships
			this.HasMany(t => t.Articles)
				.WithMany(t => t.ArticleCategories)
				.Map(m => {
						m.ToTable("ArticleCategoryRelation");
						m.MapLeftKey("ArticleCategoryID");
						m.MapRightKey("ArticleID");
					});

			this.HasRequired(t => t.User)
				.WithMany(t => t.ArticleCategories)
				.HasForeignKey(d => d.OwnerID);

		}
	}
}

