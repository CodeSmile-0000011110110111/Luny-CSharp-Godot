using Godot;
using Luny.Interfaces.Providers;
using System;

namespace Luny.Godot.Providers
{
	/// <summary>
	/// Godot implementation of application control provider.
	/// </summary>
	public sealed class GodotApplicationServiceProvider : IApplicationServiceProvider
	{
		public Boolean IsEditor => Engine.IsEditorHint();

		public Boolean IsPlaying => !Engine.IsEditorHint();

		public void Quit(Int32 exitCode = 0)
		{
			var tree = (SceneTree)Engine.GetMainLoop();
			// play nice since Godot doesn't post the close request notification by itself
			tree.Root.PropagateNotification((int)Node.NotificationWMCloseRequest);
			// prefer deferred call - we don't know when and where user may call it
			tree.CallDeferred("quit", exitCode);
		}
	}
}
