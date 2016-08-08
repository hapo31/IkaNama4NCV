using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Hapo31.IkaNama
{
	class IkaNamaException : Exception
	{
		public IkaNamaException()
		{
		}

		public IkaNamaException(string message) : base(message)
		{
		}

		public IkaNamaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected IkaNamaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
