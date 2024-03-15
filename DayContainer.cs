using Godot;
using Godot.Collections;
using System;
using Array = Godot.Collections.Array;

[Tool]
public partial class DayContainer : ColorRect
{
    [ExportCategory("Parameters")]
    [Export] private float zoomScale = 1f;
    [Export] private float minZoomScale = 1f;
    [Export] private float maxZoomScale = 10f;

    [ExportCategory("Node Refs")]
    [Export] public Area2D area;
    [Export] private CollisionShape2D collisionShape2D;
    private RectangleShape2D collisionShape;

    [Export] private VBoxContainer eventContainersContainer;

    [ExportCategory("Scenes")]
    [Export] private PackedScene eventContainerScene;

	DateTime date;

    private Array<EventContainer> eventContainers = new Array<EventContainer>();
    public override void _Ready()
    {
        collisionShape2D.Shape = new RectangleShape2D();
        collisionShape = collisionShape2D.Shape as RectangleShape2D;

        for (int i = 0; i < 24; i++)
        {
            eventContainers.Add(CreateEventContainer());
        }
    }
    public override void _Process(double delta)
    {
        area.Position = new Vector2(Size.X / 2, Size.Y / 2);
        collisionShape.Size = Size;
    }

    private EventContainer CreateEventContainer()
    {
        EventContainer container = eventContainerScene.Instantiate() as EventContainer;
        eventContainersContainer.AddChild(container);
        return container;
    }

    public void ZoomIn()
    {
        zoomScale += 0.1f;
        zoomScale = Mathf.Min(zoomScale, maxZoomScale);
        RescaleEvents();
    }

    public void ZoomOut()
    {
        zoomScale -= 0.1f;
        zoomScale = Mathf.Max(zoomScale, minZoomScale);
        RescaleEvents();
    }

    private void RescaleEvents()
    {
        foreach (EventContainer container in eventContainers)
        {
            container.CustomMinimumSize = new Vector2(
                container.CustomMinimumSize.X,
                EventContainer.baseMinimumHeight * zoomScale);
        }
    }
}
