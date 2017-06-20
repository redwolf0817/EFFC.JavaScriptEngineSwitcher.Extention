using EFFC.ChakraCore;
using JavaScriptEngineSwitcher.Core;

namespace EFFC.ChakraCore
{
	/// <summary>
	/// ChakraCore JS engine factory
	/// </summary>
	public class EFFCChakraCoreJsEngineFactory : IJsEngineFactory
	{
		/// <summary>
		/// Settings of the ChakraCore JS engine
		/// </summary>
		private readonly EFFCChakraCoreSettings _settings;

		/// <summary>
		/// Gets a name of JS engine
		/// </summary>
		public string EngineName
		{
			get { return EFFCChakraCoreJsEngine.EngineName; }
		}


		/// <summary>
		/// Constructs an instance of the ChakraCore JS engine factory
		/// </summary>
		public EFFCChakraCoreJsEngineFactory()
			: this(new EFFCChakraCoreSettings())
		{ }

		/// <summary>
		/// Constructs an instance of the ChakraCore JS engine factory
		/// </summary>
		/// <param name="settings">Settings of the ChakraCore JS engine</param>
		public EFFCChakraCoreJsEngineFactory(EFFCChakraCoreSettings settings)
		{
			_settings = settings;
		}


		/// <summary>
		/// Creates a instance of the ChakraCore JS engine
		/// </summary>
		/// <returns>Instance of the ChakraCore JS engine</returns>
		public IJsEngine CreateEngine()
		{
			return new EFFCChakraCoreJsEngine(_settings);
		}
	}
}