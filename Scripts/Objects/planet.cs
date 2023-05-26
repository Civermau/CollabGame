using Godot;
using System;

public partial class planet : CharacterBody2D
{
	public const float Speed = 100.0f;
	[Export]
	Vector2 direction = new Vector2((float)GD.Randfn(0, 1), (float)GD.Randfn(0, 1));
	public override void _Ready()
	{
		
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
