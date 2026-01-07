using Luny.Engine.Bridge;
using System;

namespace Luny.Unity.Engine.Bridge
{
	public sealed class LunyGodotPath : LunyPath
	{
		public static implicit operator LunyGodotPath(String enginePath) => new(enginePath);

		public LunyGodotPath(String nativePath)
			: base(nativePath) {}

		// Godot paths must remove their prefix
		protected override String ToEngineAgnosticPath(String nativePath) =>
			nativePath.StartsWith("res://") ? nativePath.Substring("res://".Length) : nativePath;
	}
}
