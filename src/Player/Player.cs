using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class Player : BaseCreature
{
    [Export]
    public float WalkSpeed { get; set; } = 250;

    [Export]
    public float SwimSpeed { get; set; } = 30;

    [Export]
    public float WalkDrag { get; set; } = 10;

    [Export]
    public float SwimDrag { get; set; } = 2;

    [Export]
    public bool Swimming { get; set; } = false;

    [Export]
    public bool MoveLocked { get; set; } = false;

    public List<Floor> ActiveFloors = new List<Floor>();

    Area2D groundChecker;

    public override void _Ready()
    {
        groundChecker = FindNode("GroundChecker") as Area2D;
        // groundChecker.Connect("body_entered", this, nameof(OnGroundCheckerBodyEntered));
        // groundChecker.Connect("body_exited", this, nameof(OnGroundCheckerBodyExited));

        groundChecker.Connect("area_entered", this, nameof(OnGroundCheckerAreaEntered));
        groundChecker.Connect("area_exited", this, nameof(OnGroundCheckerAreaExited));
    }


    public override void _Process(float delta)
    {
        // Development/Testing Tools
        if (Input.IsActionJustPressed("dev_toggle_swim"))
        {
            Swimming = !Swimming;
        }

        if (Input.IsActionJustPressed("dev_toggle_movelock"))
        {
            MoveLocked = !MoveLocked;
        }

        // Movement Controls
        if (!MoveLocked)
        {

            if (Input.IsActionPressed("player_move_up"))
            {
                MovementVector += Vector2.Up;
            }
            if (Input.IsActionPressed("player_move_left"))
            {
                MovementVector += Vector2.Left;
            }
            if (Input.IsActionPressed("player_move_down"))
            {
                MovementVector += Vector2.Down;
            }
            if (Input.IsActionPressed("player_move_right"))
            {
                MovementVector += Vector2.Right;
            }
        }

        // Set speed and drag based on whether the player is swimming or not
        if (Swimming)
        {
            Speed = SwimSpeed * 10 * delta;
            Drag = SwimDrag;
        }
        else
        {
            Speed = WalkSpeed * 10 * delta;
            Drag = WalkDrag;
        }

    }
    // void OnGroundCheckerBodyEntered(Node body)
    // {
    //     GD.Print($"Collided with {body}");
    //     // if i collided with an island floor
    //     // if (body is IslandFloor islandFloor) {

    //     // }
    // }
    // void OnGroundCheckerBodyExited(Node body)
    // {
    //     GD.Print($"exited {body}");
    //     // if i collided with an island floor
    //     // if (body is IslandFloor islandFloor) {

    //     // }
    // }
    void OnGroundCheckerAreaEntered(Node area)
    {
        // if i collided with an island floor
        if (area is Floor floor)
        {
            ActiveFloors.Add(floor);
            EvaluateFooting();
            GD.Print($"Collided with {floor}");
        }
    }
    void OnGroundCheckerAreaExited(Node area)
    {
        // if i left an island floor
        if (area is Floor floor)
        {
            GD.Print($"exited {floor}");
            ActiveFloors.Remove(floor);

            EvaluateFooting();
        }
    }
    void EvaluateFooting()
    {
        if (ActiveFloors.Count == 0)
        {
            Swimming = true;
        }
        else
        {
            foreach (var floor in ActiveFloors)
            {
                if (floor.Land)
                {
                    Swimming = false;
                    break;
                }
                else
                {
                    Swimming = true;
                }
            }
        }
    }
    public override void _Input(InputEvent @event)
    {

    }
}
