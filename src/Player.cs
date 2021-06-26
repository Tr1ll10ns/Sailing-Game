using Godot;
using System;

public class Player : KinematicBody2D
{
    Sprite Head;
    Sprite Body;
    Sprite Legs;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Head = GetNode<Sprite>("Hat");
        Body = GetNode<Sprite>("Shoulders");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Head.GlobalRotation += Head.GetAngleTo(GetGlobalMousePosition());
    }
}
