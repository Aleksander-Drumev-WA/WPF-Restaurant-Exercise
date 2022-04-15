﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Extensions
{
	public static class ExceptionExtensions
	{
        public static IEnumerable<string> GetInnerExceptionMessages(this Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            var innerException = ex;
            do
            {
                yield return innerException.Message;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}
