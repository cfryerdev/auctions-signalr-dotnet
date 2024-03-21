using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Exceptions
{
	public enum ExceptionType
	{
		General,
		Service,
		Validation,
		Warning,
		Authentication,
		Security,
	}
}
