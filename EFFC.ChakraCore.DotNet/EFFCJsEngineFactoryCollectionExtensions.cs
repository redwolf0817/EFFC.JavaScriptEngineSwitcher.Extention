using System;

using JavaScriptEngineSwitcher.Core;

namespace EFFC.ChakraCore
{
	/// <summary>
	/// JS engine factory collection extensions
	/// </summary>
	public static class EFFCJsEngineFactoryCollectionExtensions
	{
		/// <summary>
		/// Adds a instance of <see cref="EFFCChakraCoreJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCChakraCore(this JsEngineFactoryCollection source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			return source.AddEFFCChakraCore(new EFFCChakraCoreSettings());
		}

		/// <summary>
		/// Adds a instance of <see cref="EFFCChakraCoreJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <param name="configure">The delegate to configure the provided <see cref="EFFCChakraCoreSettings"/></param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCChakraCore(this JsEngineFactoryCollection source,
			Action<EFFCChakraCoreSettings> configure)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (configure == null)
			{
				throw new ArgumentNullException("configure");
			}

			var settings = new EFFCChakraCoreSettings();
			configure(settings);

			return source.AddEFFCChakraCore(settings);
		}

		/// <summary>
		/// Adds a instance of <see cref="EFFCChakraCoreJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <param name="settings">Settings of the ChakraCore JS engine</param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCChakraCore(this JsEngineFactoryCollection source, EFFCChakraCoreSettings settings)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			source.Add(new EFFCChakraCoreJsEngineFactory(settings));

			return source;
		}
	}
}