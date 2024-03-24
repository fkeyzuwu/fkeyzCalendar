using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public partial class Calendar : Control
{
    [ExportCategory("Node Refs")]
    [Export] private HBoxContainer daysContainer;

    [ExportCategory("Scenes")]
    [Export] private PackedScene dayContainerScene;

    private DayContainer currentHoveredDay;
    private Array<DayContainer> dayContainers = new Array<DayContainer>();
    public override void _Ready()
    {
        for(int i = 0; i < 7; i++)
        {
            dayContainers.Add(CreateDayContainer());
        }

        PopulateDayContainers();
    }

    public override void _Input(InputEvent inputEvent) 
    {
        if (inputEvent.IsActionPressed("quit")) GetTree().Quit();
        else if (inputEvent.IsAction("zoom_in_vertical"))
        {
            currentHoveredDay?.ZoomIn();
        }
        else if (inputEvent.IsAction("zoom_out_vertical"))
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

    private void PopulateDayContainers()
    {
        var today = DateTime.Today;
        for (int i = dayContainers.Count - 1; i >= 0; i--)
        {
            var dayContainer = dayContainers[i];
            DayData dayData = GetDateData(today.AddDays(i));
            dayContainer.Populate(dayData);
        }
    }

    private DayData GetDateData(DateTime date)
    {
        var dayData = GD.Load<DayData>(date.ToString());
        return dayData;
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
