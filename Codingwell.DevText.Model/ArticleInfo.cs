using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model {
	public class ArticleInfo {

		public static ArticleInfo LoadFromEntity ( ArticleEntity entity ) {
			if (entity == null)
				return default(ArticleInfo);
			ArticleInfo info = new ArticleInfo() {
				ID = entity.ID,
				Title = entity.Title,
				Path = entity.Path,
				Summary = entity.Summary,
				Content = entity.Content,
				CreateTime = entity.CreateTime,
				LastChanged = entity.LastChanged,
				State = (DbState)entity.State,
				PageViewCount = entity.PageViewCount,
				RssViewCount = entity.RssViewCount,
				EnableComment = entity.EnableComment,
				AnymousComment = entity.AnymousComment,
				DownCount = entity.DownCount,
				UpCount = entity.UpCount
			};
			return info;
		}

		/// <summary>
		/// 编号
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// 页面路径
		/// </summary>
		public string Path { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 缩略内容
		/// </summary>
		public string Summary { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime LastChanged { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public DbState State { get; set; }
		/// <summary>
		/// 页面浏览数
		/// </summary>
		public int PageViewCount { get; set; }
		/// <summary>
		/// 通过RSS及爬虫阅读数
		/// </summary>
		public int RssViewCount { get; set; }
		/// <summary>
		/// 支持数
		/// </summary>
		public int UpCount { get; set; }
		/// <summary>
		/// 反对数
		/// </summary>
		public int DownCount { get; set; }
		/// <summary>
		/// 是否允许评论
		/// </summary>
		public bool EnableComment { get; set; }
		/// <summary>
		/// 匿名留言
		/// </summary>
		public bool AnymousComment { get; set; }
	}
}
