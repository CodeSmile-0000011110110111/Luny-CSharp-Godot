using Luny.Interfaces.Providers;
using System;
using Godot;

namespace Luny.Godot.Providers
{
	/// <summary>
	/// Godot implementation of time service provider.
	/// </summary>
	public sealed class GodotTimeServiceProvider : ITimeServiceProvider
	{
		// downcast isn't an issue: even at 10,000 fps it would take nearly 30 million years before it overflows!
		public Int64 FrameCount => (long)Engine.GetProcessFrames();

		public Double ElapsedSeconds => Time.GetTicksMsec() / 1000.0;
	}
}
