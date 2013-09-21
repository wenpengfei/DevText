﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Data.Infrastructure {
	public interface IDatabaseFactory : IDisposable {
		DevTextDbContext GetDb ( );
	}
}
