using Godot;

public partial class Calendar : Control
{
    [ExportCategory("Node Refs")]
    [Export] private HBoxContainer daysContainer;

    [ExportCategory("Scenes")]
    [Export] private PackedScene dayContainerScene;

    private DayContainer currentHoveredDay;
    public override void _Ready()
    {
        for(int i = 0; i < 7; i++)
        {
            CreateDayContainer();
        }
    }

    public override void _Input(InputEvent input_event) 
    {
        if (input_event.IsActionPressed("quit")) GetTree().Quit();
        else if (input_event.IsAction("zoom_in_vertical"))
        {
            currentHoveredDay?.ZoomIn();
        }
        else if (input_event.IsAction("zoom_out_vertical"))
        {
            currentHoveredDay?.ZoomOut();
        }
    }

    private DayContainer CreateDayContainer()
    {
        DayContainer dayContainer = dayContainerScene.Instantiate() as DayContainer;
        daysContainer.AddChild(dayContainer);
        dayContainer.area.AreaEntered += (Area2D area) => _OnDayContainerMouseEntered(dayContainer);
        dayContainer.area.AreaExited += (Area2D area) => _OnDayContainerMouseExited(dayContainer);
        return dayContainer;
    }

    private void _OnDayContainerMouseEntered(DayContainer container)
    {
        currentHoveredDay = container;
    }

    private void _OnDayContainerMouseExited(DayContainer container)
    {
        if (currentHoveredDay == container) currentHoveredDay = null;
    }
}
