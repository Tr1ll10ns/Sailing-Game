using Godot;
using System;

public class BaseCreature : RigidBody2D
{
    protected Vector2 MovementVector = Vector2.Zero;

    protected float Speed = 0;

    protected float Drag = 0;

    protected float DragOffset = 0;

    [Export]
    public float MaxHealth { get; set; } = 100;

    public float Health;

    public override void _Ready()
    {
        Health = MaxHealth;
    }


    public override void _Process(float delta)
    {

    }


    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        base._IntegrateForces(state);

        // Actually move
        Move(MovementVector, Speed, state);
        MovementVector = Vector2.Zero;


        // Drag
        Vector2 dragVector = Vector2.Zero;

        // if (state.LinearVelocity.x > 0)
        // {
        // dragVector.x = -Drag * Mathf.Pow(state.LinearVelocity.x, 2);
        // }
        // else
        // {
        // dragVector.x = -Drag * -Mathf.Pow(state.LinearVelocity.x, 2);
        // }
        // if (state.LinearVelocity.y > 0)
        // {
        // dragVector.y = -Drag * Mathf.Pow(state.LinearVelocity.y, 2);
        // }
        // else
        // {
        // dragVector.y = -Drag * -Mathf.Pow(state.LinearVelocity.y, 2);
        // }
        // dragVector.x = Mathf.Clamp(dragVector.x, -Mathf.Abs(state.LinearVelocity.x), -Mathf.Abs(state.LinearVelocity.x));
        // dragVector.y = Mathf.Clamp(dragVector.y, -Mathf.Abs(state.LinearVelocity.y), -Mathf.Abs(state.LinearVelocity.y));
        dragVector.x = -Drag * state.LinearVelocity.x + DragOffset;
        dragVector.y = -Drag * state.LinearVelocity.y + DragOffset;
        state.ApplyCentralImpulse(dragVector * GetProcessDeltaTime());
    }

    public void Move(Vector2 Direction, float Speed, Physics2DDirectBodyState state)
    {
        state.ApplyCentralImpulse(Direction * Speed);
    }
    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {

    }
}
