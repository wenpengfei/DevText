using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model {
	/// <summary>
	/// 模块
	/// </summary>
	public enum ModuleType {

		News,
		Article,
		Favourite
	}


	/// <summary>
	/// 事件类型
	/// </summary>
	public enum EventType {
		/// <summary>
		/// 发布新闻
		/// </summary>
		AddNews,
		/// <summary>
		/// 发表博客
		/// </summary>
		AddArticle,
		/// <summary>
		/// 提问
		/// </summary>
		AddQuestion,
		/// <summary>
		/// 收藏
		/// </summary>
		AddBookmark,
		/// <summary>
		/// 评论新闻
		/// </summary>
		CommentNews,
		/// <summary>
		/// 评论博客
		/// </summary>
		CommentArticle,
		/// <summary>
		/// 回答提问
		/// </summary>
		AnswerQuestion
	}
}
