using System;

namespace EFFC.ChakraCore.JsRt
{
    /// <summary>
    /// The script exception
    /// </summary>
    public sealed class JsScriptException : JsException
	{
		/// <summary>
		/// The error
		/// </summary>
		private readonly JsValue _error;

		/// <summary>
		/// Gets a JavaScript object representing the script error
		/// </summary>
		public JsValue Error
		{
			get { return _error; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="JsScriptException"/> class
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="error">The JavaScript error object</param>
		public JsScriptException(JsErrorCode errorCode, JsValue error)
			: this(errorCode, error, "JavaScript Exception")
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsScriptException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="errorCode">The error code returned</param>
		/// <param name="error">The JavaScript error object</param>
		/// <param name="message">The error message</param>
		public JsScriptException(JsErrorCode errorCode, JsValue error, string message)
			: base(errorCode, message)
		{
			_error = error;
		}
	}
}