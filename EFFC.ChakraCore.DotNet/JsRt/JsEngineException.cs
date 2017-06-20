

namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The exception that occurred in the workings of the JavaScript engine itself
    /// </summary>
    public sealed class JsEngineException : JsException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsEngineException"/> class
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		public JsEngineException(JsErrorCode errorCode)
			: base(errorCode)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsEngineException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="message">The error message</param>
		public JsEngineException(JsErrorCode errorCode, string message)
			: base(errorCode, message)
		{ }
	}
}