using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.ServiceLocation;
using Codingwell.DevText.Service;

namespace UpdateUrlConsole {
	class Program {
		static void Main ( string[] args ) {

			Console.WriteLine("Update Program Starting...");
			BootStrapper.Initialise();
			Console.WriteLine("Initialise Finished");
			int count = 1;
			var news = ServiceLocator.Current.GetInstance<INewsService>().GetNews();
			Console.WriteLine(DateTime.Now);
			foreach (var item in news) {
				item.ArticleContent = item.ArticleContent.Replace("src=\"/images/", "src=\"http://img.devtext.com/");
				ServiceLocator.Current.GetInstance<INewsService>().UpdateNews(item);
				Console.WriteLine("Replaced...." + count++);
			}
			Console.WriteLine(DateTime.Now);
			Console.WriteLine("Replaced Finished");
			Console.ReadKey();
		}
	}
}
