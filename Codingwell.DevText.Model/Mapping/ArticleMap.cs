using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.Mapping
{
	public class ArticleMap : EntityTypeConfiguration<Article>
	{
		public ArticleMap()
		{
			// Primary Key
			this.HasKey(t => t.ID);

			// Properties
			this.Property(t => t.Path)
				.HasMaxLength(200);
				
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.Summary)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.ArticleContent)
				.IsRequired();
				
			this.Property(t => t.WebFrom)
				.HasMaxLength(50);
				
			this.Property(t => t.WebFromAddress)
				.HasMaxLength(500);
				
			this.Property(t => t.MetaKeywords)
				.HasMaxLength(100);
				
			this.Property(t => t.MetaDescription)
				.HasMaxLength(150);
				
			// Table & Column Mappings
			this.ToTable("Articles");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.CreatorID).HasColumnName("CreatorID");
			this.Property(t => t.Path).HasColumnName("Path");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.Summary).HasColumnName("Summary");
			this.Property(t => t.ArticleContent).HasColumnName("ArticleContent");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.LastChanged).HasColumnName("LastChanged");
			this.Property(t => t.State).HasColumnName("State");
			this.Property(t => t.PageViewCount).HasColumnName("PageViewCount");
			this.Property(t => t.RssViewCount).HasColumnName("RssViewCount");
			this.Property(t => t.UpCount).HasColumnName("UpCount");
			this.Property(t => t.DownCount).HasColumnName("DownCount");
			this.Property(t => t.EnableComment).HasColumnName("EnableComment");
			this.Property(t => t.AnymousComment).HasColumnName("AnymousComment");
			this.Property(t => t.WebFrom).HasColumnName("WebFrom");
			this.Property(t => t.WebFromAddress).HasColumnName("WebFromAddress");
			this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
			this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");

			// Relationships
			this.HasMany(t => t.Comments)
			    .WithMany(t => t.Articles)
				.Map(m =>
                    {
                        m.ToTable("ArticleCommentsRelation");
                        m.MapLeftKey("ArticleID");
                        m.MapRightKey("CommentID");
                    });
					
			this.HasRequired(t => t.User)
				.WithMany(t => t.Articles)
				.HasForeignKey(d => d.CreatorID);
				
		}
	}
}

