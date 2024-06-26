﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Auctioneer.Logic.Exceptions
{
	public class HandledException : Exception
	{
		public string ErrorCode { get; set; }
		public ExceptionType ExceptionType { get; set; }
		public HttpStatusCode StatusCode { get; set; }

		public const string DefaultErrorCode = "ERR001";

		/// <summary>
		/// The returned exception type used by the frontend to detect if 
		/// this is handled by our notification service, or should be 
		/// redirected to an oops page.
		/// </summary>
		public const string ReturnedExceptionType = "HandledException";

		public List<HandledException> InnerExceptions { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HandledException"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="status">The status.</param>
		public HandledException(ExceptionType type, string message, HttpStatusCode status = HttpStatusCode.BadRequest, string errorCode = DefaultErrorCode) : base(message)
		{
			ErrorCode = errorCode;
			ExceptionType = type;
			StatusCode = status;
			InnerExceptions = new List<HandledException>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HandledException"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="status">The status.</param>
		public HandledException(ExceptionType type, string message, HandledException exception, HttpStatusCode status = HttpStatusCode.BadRequest) : base(message, exception)
		{
			ErrorCode = DefaultErrorCode;
			ExceptionType = type;
			StatusCode = status;
			InnerExceptions = new List<HandledException>();
			InnerExceptions.Add(exception);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HandledException"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="exceptions">The exceptions.</param>
		/// <param name="status">The status.</param>
		public HandledException(ExceptionType type, string message, List<HandledException> exceptions, HttpStatusCode status = HttpStatusCode.BadRequest) : base(message)
		{
			ExceptionType = type;
			StatusCode = status;
			InnerExceptions = exceptions;
		}

		/// <summary>
		/// Gets the exception list in a readable form for restful consumption.
		/// </summary>
		/// <returns></returns>
		public List<ExceptionListItem> GetExceptionList()
		{
			var collection = new List<ExceptionListItem>();
			if (InnerExceptions != null && InnerExceptions.Any())
			{
				InnerExceptions.ForEach(ex => collection.Add(new ExceptionListItem() { Name = ex.Message, Type = ex.ExceptionType.ToString() }));
			}
			else
			{
				collection.Add(new ExceptionListItem() { Name = this.Message, Type = this.ExceptionType.ToString() });
			}
			return collection;
		}
	}

	public class ExceptionListItem
	{
		public string Name { get; set; }
		public string Type { get; set; }
	}

}
