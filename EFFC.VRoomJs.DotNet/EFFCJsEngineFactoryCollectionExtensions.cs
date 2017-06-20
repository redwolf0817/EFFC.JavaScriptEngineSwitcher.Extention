using System;

using JavaScriptEngineSwitcher.Core;

namespace EFFC.VRoomJs
{
	/// <summary>
	/// JS engine factory collection extensions
	/// </summary>
	public static class EFFCJsEngineFactoryCollectionExtensions
	{
		/// <summary>
		/// Adds a instance of <see cref="EFFCVroomJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCVroom(this JsEngineFactoryCollection source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			return source.AddEFFCVroom(new EFFCVroomSettings());
		}

		/// <summary>
		/// Adds a instance of <see cref="EFFCVroomJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <param name="configure">The delegate to configure the provided <see cref="EFFCVroomSettings"/></param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCVroom(this JsEngineFactoryCollection source,
			Action<EFFCVroomSettings> configure)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (configure == null)
			{
				throw new ArgumentNullException("configure");
			}

			var settings = new EFFCVroomSettings();
			configure(settings);

			return source.AddEFFCVroom(settings);
		}

		/// <summary>
		/// Adds a instance of <see cref="EFFCVroomJsEngineFactory"/> to
		/// the specified <see cref="JsEngineFactoryCollection" />
		/// </summary>
		/// <param name="source">Instance of <see cref="JsEngineFactoryCollection" /></param>
		/// <param name="settings">Settings of the Vroom JS engine</param>
		/// <returns>Instance of <see cref="JsEngineFactoryCollection" /></returns>
		public static JsEngineFactoryCollection AddEFFCVroom(this JsEngineFactoryCollection source,
			EFFCVroomSettings settings)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			source.Add(new EFFCVroomJsEngineFactory(settings));

			return source;
		}
	}
}