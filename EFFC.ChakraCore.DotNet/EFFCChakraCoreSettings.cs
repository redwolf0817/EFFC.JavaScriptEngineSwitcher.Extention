namespace EFFC.ChakraCore
{
	/// <summary>
	/// Settings of the ChakraCore JS engine
	/// </summary>
	public sealed class EFFCChakraCoreSettings
	{
		/// <summary>
		/// Gets or sets a flag for whether to disable any background work (such as garbage collection)
		/// on background threads
		/// </summary>
		public bool DisableBackgroundWork
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to disable native code generation
		/// </summary>
		public bool DisableNativeCodeGeneration
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to disable calls of <code>eval</code> function
		/// </summary>
		public bool DisableEval
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a flag for whether to enable all experimental features
		/// </summary>
		public bool EnableExperimentalFeatures
		{
			get;
			set;
		}


		/// <summary>
		/// Constructs instance of the ChakraCore settings
		/// </summary>
		public EFFCChakraCoreSettings()
		{
			DisableBackgroundWork = false;
			DisableNativeCodeGeneration = false;
			DisableEval = false;
			EnableExperimentalFeatures = false;
		}
	}
}