﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Exceptions
{
	public class HandledResponseError
	{
		public string Code { get; set; }
		public string Type { get; set; }
		public string Message { get; set; }
	}
}
