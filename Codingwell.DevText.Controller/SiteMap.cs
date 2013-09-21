using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using MvcSiteMapProvider.Extensibility;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller {
	public class NewsDetailsDynamicNodeProvider
	: DynamicNodeProviderBase {

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection ( ) {
			var returnValue = new List<DynamicNode>();
			var news = DependencyResolver.Current.GetService<INewsService>().GetNews();
			foreach (var article in news) {
				DynamicNode node = new DynamicNode();
				node.Title = article.Title;
				node.RouteValues.Add("id", article.ID);
				if (!string.IsNullOrEmpty(article.Path)) {
					node.RouteValues.Add("path", article.Path);
				}
				returnValue.Add(node); 
			}
			return returnValue; 
		}

		public override CacheDescription GetCacheDescription ( ) {
			return new CacheDescription("NewsDetailsDynamicNodeProvider") {
				SlidingExpiration=TimeSpan.FromHours(12)
			};
		}
	}

	public class NewsCategoryDynamicNodeProvider
	: DynamicNodeProviderBase {

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection ( ) {
			var returnValue = new List<DynamicNode>();
			var news = DependencyResolver.Current.GetService<INewsService>().GetCategories();
			foreach (var article in news) {
				DynamicNode node = new DynamicNode();
				node.Title = article.Name;
				node.RouteValues.Add("id", article.ID);
				returnValue.Add(node);
			}
			return returnValue;
		}

		public override CacheDescription GetCacheDescription ( ) {
			return new CacheDescription("NewsCategoryDynamicNodeProvider") {
				SlidingExpiration = TimeSpan.FromHours(12)
			};
		}
	}

	public class NewsTopicDynamicNodeProvider
	: DynamicNodeProviderBase {

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection ( ) {
			var returnValue = new List<DynamicNode>();
			var news = DependencyResolver.Current.GetService<INewsTopicService>().GetAllNewsTopics();
			foreach (var article in news) {
				DynamicNode node = new DynamicNode();
				node.Title = article.Name;
				node.RouteValues.Add("id", article.ID);
				returnValue.Add(node);
			}
			return returnValue;
		}

		public override CacheDescription GetCacheDescription ( ) {
			return new CacheDescription("NewsTopicDynamicNodeProvider") {
				SlidingExpiration = TimeSpan.FromHours(12)
			};
		}
	}

	public class BlogDetailsDynamicNodeProvider
	: DynamicNodeProviderBase {

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection ( ) {
			var returnValue = new List<DynamicNode>();
			var news = DependencyResolver.Current.GetService<IArticleService>().GetArticles();
			foreach (var article in news) {
				DynamicNode node = new DynamicNode();
				node.Title = article.Title;
				node.RouteValues.Add("id", article.ID);
				if (!string.IsNullOrEmpty(article.Path)) {
					node.RouteValues.Add("path", article.Path);
				}
				returnValue.Add(node);
			}
			return returnValue;
		}

		public override CacheDescription GetCacheDescription ( ) {
			return new CacheDescription("BlogDetailsDynamicNodeProvider") {
				SlidingExpiration = TimeSpan.FromHours(12)
			};
		}
	}

	public class UserDynamicNodeProvider
	: DynamicNodeProviderBase {

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection ( ) {
			var returnValue = new List<DynamicNode>();
			var users = DependencyResolver.Current.GetService<IUserService>().GetUsers();
			foreach (var user in users) {
				DynamicNode node = new DynamicNode();
				node.Title = user.NickName;
				node.RouteValues.Add("userid", user.UserID);
				returnValue.Add(node);
			}
			return returnValue;
		}

		public override CacheDescription GetCacheDescription ( ) {
			return new CacheDescription("UserDynamicNodeProvider") {
				SlidingExpiration = TimeSpan.FromHours(12)
			};
		}
	}
}
