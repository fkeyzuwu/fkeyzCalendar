using Godot;
using System;

[Tool]
public partial class Mouse : Node2D
{
	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
	}
}
