using System;

namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The API usage exception occurred
    /// </summary>
    public sealed class JsUsageException : JsException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsUsageException"/> class
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		public JsUsageException(JsErrorCode errorCode)
			: base(errorCode)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsUsageException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="message">The error message</param>
		public JsUsageException(JsErrorCode errorCode, string message)
			: base(errorCode, message)
		{ }

	}
}