using Godot;
using System;

public class Floor : Area2D
{
    // [Export]
    // public float FrictionMultiplier { get; set; } = 1;

    [Export]
    public bool Land { get; set; } = true;

    public override void _Ready()
    {

    }
}
