using Godot;
using Luny.Proxies;
using System;
using SystemObject = System.Object;

namespace Luny.Godot.Proxies
{
	/// <summary>
	/// Godot-specific implementation wrapping Godot.Node.
	/// </summary>
	public sealed class GodotNode : LunyObject
	{
		private readonly Node _node;

		public override String Name => _node != null ? _node.Name : "<null>";
		public override Boolean IsValid => _node != null && GodotObject.IsInstanceValid(_node);

		/// <summary>
		/// Gets the wrapped Godot Node.
		/// </summary>
		public Node Node => _node;

		public GodotNode(Node node) => _node = node;

		public override SystemObject GetNativeObject() => _node;
	}
}
