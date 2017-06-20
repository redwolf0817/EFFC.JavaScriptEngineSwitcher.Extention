
namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The fatal exception occurred
    /// </summary>
    public sealed class JsFatalException : JsException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsFatalException"/> class
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		public JsFatalException(JsErrorCode errorCode)
			: base(errorCode)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsFatalException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="message">The error message</param>
		public JsFatalException(JsErrorCode errorCode, string message)
			: base(errorCode, message)
		{ }
	}
}