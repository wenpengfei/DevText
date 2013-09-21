using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model {
	[Serializable]
	public class QUserJsonModel {
		public QUserJsonModel ( ) { }

		public string Figureurl { get; set; }
		public string Figureurl_1 { get; set; }
		public string Figureurl_2 { get; set; }
		public string Msg { get; set; }
		public string Nickname { get; set; }
		public int Ret { get; set; }
	}
}
