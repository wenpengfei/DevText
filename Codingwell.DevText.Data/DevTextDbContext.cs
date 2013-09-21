using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Mapping;

namespace Codingwell.DevText.Data {
	public class DevTextDbContext : DbContext {

		static DevTextDbContext ( ) {
			Database.SetInitializer<DevTextDbContext>(null);
		}
		public DevTextDbContext ( )
			: base("Name=DevTextDbContext") {
		}

		public DbSet<ActionChannel> ActionChannels { get; set; }
		public DbSet<Action> Actions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<ArticleCategory> ArticleCategories { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<Bookmark> Bookmarks { get; set; }
		public DbSet<BuildLog> BuildLogs { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<EventLog> EventLogs { get; set; }
		public DbSet<HomeContent> HomeContents { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<NewsCategory> NewsCategories { get; set; }
		public DbSet<NewsTopic> NewsTopics { get; set; }
		public DbSet<OpenAuth> OpenAuths { get; set; }
		public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<QuestionSupplement> QuestionSupplements { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<SysLog> SysLogs { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating ( DbModelBuilder modelBuilder ) {
			modelBuilder.Configurations.Add(new ActionChannelMap());
			modelBuilder.Configurations.Add(new ActionMap());
			modelBuilder.Configurations.Add(new AnswerMap());
			modelBuilder.Configurations.Add(new ArticleCategoryMap());
			modelBuilder.Configurations.Add(new ArticleMap());
			modelBuilder.Configurations.Add(new BookmarkMap());
			modelBuilder.Configurations.Add(new BuildLogMap());
			modelBuilder.Configurations.Add(new CommentMap());
			modelBuilder.Configurations.Add(new EventLogMap());
			modelBuilder.Configurations.Add(new HomeContentMap());
			modelBuilder.Configurations.Add(new NewsMap());
			modelBuilder.Configurations.Add(new NewsCategoryMap());
			modelBuilder.Configurations.Add(new NewsTopicMap());
			modelBuilder.Configurations.Add(new OpenAuthMap());
			modelBuilder.Configurations.Add(new QuestionAnswerMap());
			modelBuilder.Configurations.Add(new QuestionMap());
			modelBuilder.Configurations.Add(new QuestionSupplementMap());
			modelBuilder.Configurations.Add(new RoleMap());
			modelBuilder.Configurations.Add(new SysLogMap());
			modelBuilder.Configurations.Add(new UserMap());
		}
	}
}
