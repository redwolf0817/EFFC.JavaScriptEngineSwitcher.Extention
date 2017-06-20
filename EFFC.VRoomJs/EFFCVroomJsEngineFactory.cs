using JavaScriptEngineSwitcher.Core;

namespace EFFC.VRoomJs
{
	/// <summary>
	/// Vroom JS engine factory
	/// </summary>
	public sealed class EFFCVroomJsEngineFactory : IJsEngineFactory
	{
		/// <summary>
		/// Settings of the Vroom JS engine
		/// </summary>
		private readonly EFFCVroomSettings _settings;

		/// <summary>
		/// Gets a name of JS engine
		/// </summary>
		public string EngineName
		{
			get { return EFFCVroomJsEngine.EngineName; }
		}


		/// <summary>
		/// Constructs an instance of the Vroom JS engine factory
		/// </summary>
		public EFFCVroomJsEngineFactory()
			: this(new EFFCVroomSettings())
		{ }

		/// <summary>
		/// Constructs an instance of the Vroom JS engine factory
		/// </summary>
		/// <param name="settings">Settings of the Vroom JS engine</param>
		public EFFCVroomJsEngineFactory(EFFCVroomSettings settings)
		{
			_settings = settings;
		}


		/// <summary>
		/// Creates a instance of the Vroom JS engine
		/// </summary>
		/// <returns>Instance of the Vroom JS engine</returns>
		public IJsEngine CreateEngine()
		{
			return new EFFCVroomJsEngine(_settings);
		}
	}
}