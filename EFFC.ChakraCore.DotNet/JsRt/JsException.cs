using System;
#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif

namespace EFFC.ChakraCore.JsRt
{
	/// <summary>
	/// The exception returned from the Chakra engine
	/// </summary>
#if !NETSTANDARD1_3
	[Serializable]
#endif
    public class JsException : Exception
	{
		/// <summary>
		/// The error code
		/// </summary>
		private readonly JsErrorCode _errorCode;

		/// <summary>
		/// Gets a error code
		/// </summary>
		public JsErrorCode ErrorCode
		{
			get { return _errorCode; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="JsException"/> class
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		public JsException(JsErrorCode errorCode)
			: this(errorCode, "A fatal exception has occurred in a JavaScript runtime")
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="message">The error message</param>
		public JsException(JsErrorCode errorCode, string message)
			: base(message)
		{
			_errorCode = errorCode;
		}
	}
}