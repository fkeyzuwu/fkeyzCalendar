using Godot;
using System;

[Tool]
public partial class EventContainer : ColorRect
{
    public static int baseMinimumHeight = 38;
    [Export] private TextEdit info;
    [Export] public Area2D area;
    [Export] private CollisionShape2D collisionShape2D;
    private RectangleShape2D collisionShape;

    private static EventContainer playerHover = null;
    private bool pickingColor = false;
    public override void _Ready()
    {
        collisionShape2D.Shape = new RectangleShape2D();
        collisionShape = collisionShape2D.Shape as RectangleShape2D;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("right_click") && playerHover == this)
        {
            pickingColor = true;
        }
        else if(inputEvent.IsActionReleased("right_click") && pickingColor)
        {
            pickingColor = false;
        }
        else if(pickingColor && inputEvent is InputEventMouseMotion mouseMotionEvent)
        {
            Color color = (Color)info.Get("theme_override_colors/background_color");
            info.Set("theme_override_colors/background_color", new Color(
                    color.R + mouseMotionEvent.Relative.X / 256f,
                    color.G + mouseMotionEvent.Relative.Y / 256f,
                    Color.B));
        }
    }

    public override void _Process(double delta)
    {
        area.Position = new Vector2(Size.X / 2, Size.Y / 2) ;
        collisionShape.Size = Size;
    }

    public void _on_mouse_area_entered(Area2D area)
    {
        playerHover = this;
    }

    public void _on_mouse_area_exited(Area2D area)
    {
        if(playerHover == this) playerHover = null;
    }
}