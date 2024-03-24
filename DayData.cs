using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class DayData : Resource
{
    public DateTime dateTime;
    public List<DayEvent> dayEvents;
}

public struct DayEvent
{
    public DateTime dateTime;
    public int hourStart;
    public int minuteStart;
    public int hourEnd;
    public int minuteEnd;
    public string text;
}