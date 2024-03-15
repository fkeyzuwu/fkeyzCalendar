using Godot;
using System;

[Tool]
public partial class EventContainer : ColorRect
{
    public static int baseMinimumHeight = 38;
    [Export] public Area2D area;
    [Export] private CollisionShape2D collisionShape2D;
    private RectangleShape2D collisionShape;

    public override void _Ready()
    {
        collisionShape2D.Shape = new RectangleShape2D();
        collisionShape = collisionShape2D.Shape as RectangleShape2D;
    }

    public override void _Process(double delta)
    {
        area.Position = new Vector2(Size.X / 2, Size.Y / 2) ;
        collisionShape.Size = Size;
    }
}