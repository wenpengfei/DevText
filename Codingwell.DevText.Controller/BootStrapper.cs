using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller {
	public class BootStrapper {
		public static void Initialise ( ) {

			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.Register(c => new DatabaseFactory()).As<IDatabaseFactory>().InstancePerLifetimeScope();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
			builder.RegisterType<HomeContentService>().As<IHomeContentService>().InstancePerLifetimeScope();
			builder.RegisterType<ActionService>().As<IActionService>().InstancePerLifetimeScope();
			builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
			builder.RegisterType<BookmarkService>().As<IBookmarkService>().InstancePerLifetimeScope();
			builder.RegisterType<BuildLogService>().As<IBuildLogService>().InstancePerLifetimeScope();
			builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();
			builder.RegisterType<NewsTopicService>().As<INewsTopicService>().InstancePerLifetimeScope();
			builder.RegisterType<OpenAuthService>().As<IOpenAuthService>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionAnswerService>().As<IQuestionAnswerService>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionSupplementService>().As<IQuestionSupplementService>().InstancePerLifetimeScope();
			builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
			builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
			builder.RegisterType<HomeContentRepository>().As<IHomeContentRepository>().InstancePerLifetimeScope();
			builder.RegisterType<ActionChannelRepository>().As<IActionChannelRepository>().InstancePerLifetimeScope();
			builder.RegisterType<ActionRepository>().As<IActionRepository>().InstancePerLifetimeScope();
			builder.RegisterType<ArticleCategoryRepository>().As<IArticleCategoryRepository>().InstancePerLifetimeScope();
			builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();
			builder.RegisterType<BookmarkRepository>().As<IBookmarkRepository>().InstancePerLifetimeScope();
			builder.RegisterType<BuildLogRepository>().As<IBuildLogRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NewsCategoryRepository>().As<INewsCategoryRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NewsRepository>().As<INewsRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NewsTopicRepository>().As<INewsTopicRepository>().InstancePerLifetimeScope();
			builder.RegisterType<OpenAuthRepository>().As<IOpenAuthRepository>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionAnswerRepository>().As<IQuestionAnswerRepository>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionRepository>().As<IQuestionRepository>().InstancePerLifetimeScope();
			builder.RegisterType<QuestionSupplementRepository>().As<IQuestionSupplementRepository>().InstancePerLifetimeScope();
			builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
			builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

			IContainer container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

		}
	}
}
