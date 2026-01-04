using Luny.Engine.Bridge;
using System;
using Native = Godot;

namespace Luny.Godot.Engine.Bridge
{
	/// <summary>
	/// Godot-specific implementation wrapping Godot.Node.
	/// </summary>
	internal sealed class GodotNode : LunyObject
	{
		private Native.Node.ProcessModeEnum _enabledProcessMode;

		private Native.Node Node => Cast<Native.Node>();

		private static Boolean IsNodeVisible(Native.Node node) => node switch
		{
			Native.Node3D n3d => n3d.Visible,
			Native.CanvasItem ci => ci.Visible,
			Native.CanvasLayer cl => cl.Visible,
			var _ => false,
		};

		private static void SetNodeVisible(Native.Node node, Boolean visible)
		{
			switch (node)
			{
				case Native.Node3D n3d:
					n3d.Visible = visible;
					break;
				case Native.CanvasItem ci:
					ci.Visible = visible;
					break;
				case Native.CanvasLayer cl:
					cl.Visible = visible;
					break;
			}
		}

		public GodotNode(Native.Node node)
			: base(node, (Int64)node.GetInstanceId(), node.ProcessMode != Native.Node.ProcessModeEnum.Disabled, IsNodeVisible(node)) =>
			SetDefaultEnabledMode(node);

		// if the node starts Disabled, we default to Inherit when we wish to return to its "default" state
		// otherwise a node starting Disabled would be .. hold your breath .. unenableable! :)
		private void SetDefaultEnabledMode(Native.Node node) => _enabledProcessMode = node.ProcessMode == Native.Node.ProcessModeEnum.Disabled
			? Native.Node.ProcessModeEnum.Inherit
			: node.ProcessMode;

		protected override void DestroyNativeObject() => Node?.QueueFree();
		protected override Boolean IsNativeObjectValid() => Node is {} node && Native.GodotObject.IsInstanceValid(node) && node.IsInsideTree();
		protected override String GetNativeObjectName() => Node.Name;
		protected override void SetNativeObjectName(String name) => Node.Name = name;
		protected override Boolean GetNativeObjectEnabledInHierarchy() => Node.CanProcess();
		protected override Boolean GetNativeObjectEnabled() => Node.ProcessMode != Native.Node.ProcessModeEnum.Disabled;

		protected override void SetNativeObjectEnabled()
		{
			var node = Node;
			node.ProcessMode = _enabledProcessMode;
			SetNodeVisible(node, true); // follows Unity's "active" model
		}

		protected override void SetNativeObjectDisabled()
		{
			var node = Node;
			node.ProcessMode = Native.Node.ProcessModeEnum.Disabled;
			SetNodeVisible(node, false); // follows Unity's "active" model
		}

		protected override void SetNativeObjectVisible() => SetNodeVisible(Node, true);
		protected override void SetNativeObjectInvisible() => SetNodeVisible(Node, false);
	}
}
