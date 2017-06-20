using System;
using System.Collections.Generic;
using System.Text;

using OriginalAssemblyLoader = VroomJs.AssemblyLoader;
using OriginalJsContext = VroomJs.JsContext;
using OriginalJsEngine = VroomJs.JsEngine;
using OriginalJsException = VroomJs.JsException;
using OriginalJsInteropException = VroomJs.JsInteropException;

using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Core.Utilities;
using CoreStrings = JavaScriptEngineSwitcher.Core.Resources.Strings;

using EFFC.VRoomJs.Utilities;
using VroomJs;

namespace EFFC.VRoomJs
{
	/// <summary>
	/// Adapter for the Vroom JS engine (cross-platform bridge to the V8 JS engine)
	/// </summary>
	public sealed class EFFCVroomJsEngine : JsEngineBase
	{
		/// <summary>
		/// Name of JS engine
		/// </summary>
		public const string EngineName = "VroomJsEngine";

		/// <summary>
		/// Version of original JS engine
		/// </summary>
		private const string EngineVersion = "3.17.16.2";

		/// <summary>
		/// Vroom JS engine
		/// </summary>
		private OriginalJsEngine _jsEngine;

		/// <summary>
		/// JS context
		/// </summary>
		private OriginalJsContext _jsContext;

		/// <summary>
		/// Synchronizer of code execution
		/// </summary>
		private readonly object _executionSynchronizer = new object();

		/// <summary>
		/// List of host items
		/// </summary>
		private readonly Dictionary<string, object> _hostItems = new Dictionary<string, object>();

		/// <summary>
		/// Unique document name manager
		/// </summary>
		private readonly UniqueDocumentNameManager _documentNameManager =
			new UniqueDocumentNameManager(DefaultDocumentName);

		/// <summary>
		/// Gets a name of JS engine
		/// </summary>
		public override string Name
		{
			get { return EngineName; }
		}

		/// <summary>
		/// Gets a version of original JS engine
		/// </summary>
		public override string Version
		{
			get { return EngineVersion; }
		}

		/// <summary>
		/// Gets a value that indicates if the JS engine supports garbage collection
		/// </summary>
		public override bool SupportsGarbageCollection
		{
			get { return false; }
		}

        /// <summary>
        /// Current JsContext
        /// added by chuan.yin in 2017/4/17
        /// </summary>
        public OriginalJsContext CurrentContext
        {
            get
            {
                return _jsContext;
            }
        }
        /// <summary>
        /// Static constructor
        /// </summary>
        static EFFCVroomJsEngine()
		{
			if (Utils.IsWindows())
			{
				OriginalAssemblyLoader.EnsureLoaded();
			}
		}

		/// <summary>
		/// Constructs an instance of adapter for the Vroom JS engine
		/// (cross-platform bridge to the V8 JS engine)
		/// </summary>
		public EFFCVroomJsEngine()
			: this(new EFFCVroomSettings())
		{ }

		/// <summary>
		/// Constructs an instance of adapter for the Vroom JS engine
		/// (cross-platform bridge to the V8 JS engine)
		/// </summary>
		/// <param name="settings">Settings of the Vroom JS engine</param>
		public EFFCVroomJsEngine(EFFCVroomSettings settings)
		{
			EFFCVroomSettings vroomSettings = settings ?? new EFFCVroomSettings();

			try
			{
				_jsEngine = new OriginalJsEngine(vroomSettings.MaxYoungSpaceSize,
					vroomSettings.MaxOldSpaceSize);
				_jsContext = _jsEngine.CreateContext();
			}
			catch (Exception e)
			{
				throw new JsEngineLoadException(
					string.Format(CoreStrings.Runtime_JsEngineNotLoaded,
						EngineName, e.Message), EngineName, EngineVersion, e);
			}
		}


		/// <summary>
		/// Makes a mapping from the host type to a Vroom type
		/// </summary>
		/// <param name="value">The source value</param>
		/// <returns>The mapped value</returns>
		private static object MapToVroomType(object value)
		{
			if (value is Undefined)
			{
				return null;
			}

			return value;
		}

		private static JsRuntimeException ConvertJsExceptionToJsRuntimeException(
			OriginalJsException jsException)
		{
			string message = jsException.Message;
			string category;
			int lineNumber = 0;
			int columnNumber = 0;
			string sourceFragment = string.Empty;

			if (jsException is OriginalJsInteropException)
			{
				category = "InteropError";
			}
			else
			{
				category = jsException.Type;
				lineNumber = jsException.Line;
				columnNumber = jsException.Column;
			}

			var jsRuntimeException = new JsRuntimeException(message, EngineName, EngineVersion,
				jsException)
			{
				Category = category,
				LineNumber = lineNumber,
				ColumnNumber = columnNumber,
				SourceFragment = sourceFragment
			};

			return jsRuntimeException;
		}

		#region JsEngineBase implementation

		protected override object InnerEvaluate(string expression)
		{
			return InnerEvaluate(expression, null);
		}

		protected override object InnerEvaluate(string expression, string documentName)
		{
			object result;
			string uniqueDocumentName = _documentNameManager.GetUniqueName(documentName);

			lock (_executionSynchronizer)
			{
				try
				{
                    //modified by chuan.yin in 2017/5/16
					var jsobj = _jsContext.Execute(expression, uniqueDocumentName);
                    result = Convert2Object(CurrentContext, jsobj);
				}
				catch (OriginalJsException e)
				{
					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}

			return result;
		}

		protected override T InnerEvaluate<T>(string expression)
		{
			return InnerEvaluate<T>(expression, null);
		}

		protected override T InnerEvaluate<T>(string expression, string documentName)
		{
			object result = InnerEvaluate(expression, documentName);
            var netobj = Convert2Object(CurrentContext, result)
;			return TypeConverter.ConvertToType<T>(netobj);
		}

		protected override void InnerExecute(string code)
		{
			InnerExecute(code, null);
		}

		protected override void InnerExecute(string code, string documentName)
		{
			string uniqueDocumentName = _documentNameManager.GetUniqueName(documentName);

			lock (_executionSynchronizer)
			{
				try
				{
					_jsContext.Execute(code, uniqueDocumentName);
				}
				catch (OriginalJsException e)
				{
					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}
		}

		protected override object InnerCallFunction(string functionName, params object[] args)
		{
			string serializedArguments = string.Empty;
			int argumentCount = args.Length;

			if (argumentCount == 1)
			{
				object value = args[0];
				serializedArguments = SimplisticJsSerializer.Serialize(value);
			}
			else if (argumentCount > 1)
			{
				var serializedArgumentsBuilder = new StringBuilder();

				for (int argumentIndex = 0; argumentIndex < argumentCount; argumentIndex++)
				{
					object value = args[argumentIndex];
					string serializedValue = SimplisticJsSerializer.Serialize(value);

					if (argumentIndex > 0)
					{
						serializedArgumentsBuilder.Append(", ");
					}
					serializedArgumentsBuilder.Append(serializedValue);
				}

				serializedArguments = serializedArgumentsBuilder.ToString();
			}

			object result = Evaluate(string.Format("{0}({1});", functionName, serializedArguments));

			return result;
		}

		protected override T InnerCallFunction<T>(string functionName, params object[] args)
		{
			object result = InnerCallFunction(functionName, args);

			return TypeConverter.ConvertToType<T>(result);
		}

		protected override bool InnerHasVariable(string variableName)
		{
			string expression = string.Format("(typeof {0} !== 'undefined');", variableName);
			var result = InnerEvaluate<bool>(expression);

			return result;
		}

		protected override object InnerGetVariableValue(string variableName)
		{
			object result;

			lock (_executionSynchronizer)
			{
				try
				{
                    //modified by chuan.yin in 2017/5/16
					var jsobj = _jsContext.GetVariable(variableName);
                    result = Convert2Object(CurrentContext, jsobj);
				}
				catch (OriginalJsException e)
				{
					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}

			return result;
		}

		protected override T InnerGetVariableValue<T>(string variableName)
		{
			object result = InnerGetVariableValue(variableName);
            //added by chuan.yin in 2017/5/16
            var netobj = Convert2Object(CurrentContext, result);
            return TypeConverter.ConvertToType<T>(netobj);
		}

		protected override void InnerSetVariableValue(string variableName, object value)
		{
			object processedValue = MapToVroomType(value);

			lock (_executionSynchronizer)
			{
				try
				{
					_jsContext.SetVariable(variableName, processedValue);
				}
				catch (OriginalJsException e)
				{
					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}
		}

		protected override void InnerRemoveVariable(string variableName)
		{
			string expression = string.Format(@"if (typeof {0} !== 'undefined') {{
	{0} = undefined;
}}", variableName);

			lock (_executionSynchronizer)
			{
				try
				{
					_jsContext.Execute(expression);

					if (_hostItems.ContainsKey(variableName))
					{
						_hostItems.Remove(variableName);
					}
				}
				catch (OriginalJsException e)
				{
					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}
		}

		private void EmbedHostItem(string itemName, object value)
		{
			lock (_executionSynchronizer)
			{
				object oldValue = null;
				if (_hostItems.ContainsKey(itemName))
				{
					oldValue = _hostItems[itemName];
				}
				_hostItems[itemName] = value;

				try
				{
					var delegateValue = value as Delegate;
					if (delegateValue != null)
					{
						_jsContext.SetFunction(itemName, delegateValue);
					}
					else
					{
						_jsContext.SetVariable(itemName, value);
					}
				}
				catch (OriginalJsException e)
				{
					if (oldValue != null)
					{
						_hostItems[itemName] = oldValue;
					}
					else
					{
						_hostItems.Remove(itemName);
					}

					throw ConvertJsExceptionToJsRuntimeException(e);
				}
			}
		}

		protected override void InnerEmbedHostObject(string itemName, object value)
		{
			object processedValue = MapToVroomType(value);
			EmbedHostItem(itemName, processedValue);
		}

		protected override void InnerEmbedHostType(string itemName, Type type)
		{
			EmbedHostItem(itemName, type);
		}

		protected override void InnerCollectGarbage()
		{
			throw new NotImplementedException();
		}
        /// <summary>
        /// 将js对象转化成.net对象
        /// added by chuan.yin in 2017/5/16
        /// </summary>
        /// <param name="context"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object Convert2Object(OriginalJsContext context, object obj)
        {
            if (obj is JsObject)
            {
                var dic = new Dictionary<string, object>();
                var jo = (JsObject)obj;
                foreach (var item in context.GetMemberNames(jo))
                {
                    var v = context.GetPropertyValue(jo, item);
                    dic.Add(item, Convert2Object(context, v));
                }

                return dic;
            }
            else if (obj is DateTime)
            {
                return ((DateTime)obj).ToLocalTime();
            }
            else if (obj is JsFunction)
            {
                var jf = (JsFunction)obj;
                return jf.ToString();
            }
            else
            {
                return obj;
            }
        }
        #endregion

        #region IDisposable implementation

        public override void Dispose()
		{
			if (_disposedFlag.Set())
			{
				lock (_executionSynchronizer)
				{
					if (_jsContext != null)
					{
						_jsContext.Dispose();
						_jsContext = null;
					}

					if (_jsEngine != null)
					{
						_jsEngine.Dispose();
						_jsEngine = null;
					}

					if (_hostItems != null)
					{
						_hostItems.Clear();
					}
				}
			}
		}

		#endregion
	}
}