using Godot;
using Luny.Providers;
using System;
using System.IO;

namespace Luny.Godot.Providers
{
	/// <summary>
	/// Godot implementation of scene information provider.
	/// </summary>
	public sealed class GodotSceneServiceProvider : ISceneServiceProvider
	{
		public String CurrentSceneName
		{
			get
			{
				var tree = (SceneTree)Engine.GetMainLoop();
				var currentScene = tree?.CurrentScene;
				return currentScene?.Name ?? Path.GetFileNameWithoutExtension(currentScene?.SceneFilePath) ?? String.Empty;
			}
		}
	}
}
