using Luny.Engine.Bridge;
using System;
using Node = Godot.Node;

namespace Luny.Unity.Engine.Bridge
{
	public sealed class LunyGodotScene : LunyScene
	{
		private String _name;
		public override String Name => _name ??= System.IO.Path.GetFileNameWithoutExtension(((Node)NativeScene)?.SceneFilePath);

		public LunyGodotScene(Node nativeScene)
			: base(nativeScene, new LunyGodotPath(nativeScene.SceneFilePath)) {}
	}
}
