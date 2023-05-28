using Godot;
using System;

public partial class sun : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	private Vector2 sizeMultiplier = new Vector2(1, 1);
	[Export]
	private Vector2 sizeBase = new Vector2(1, 1);
	[Export]
	private int maxPlanetCount = 0;

	public override void _Ready()
	{
		GD.Print(Name + " succesfully loaded!");

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("moveSunLeft", "moveSunRight", "moveSunUp", "moveSunDown");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.Lerp(Velocity.X, 0, 0.05f);
			velocity.Y = Mathf.Lerp(Velocity.Y, 0, 0.05f);
		}

		Velocity = velocity;
		MoveAndSlide();
		updateSize();

	}
	private void updateSize()
	{
		this.Scale = sizeBase * sizeMultiplier;
	}
	//hello
	
}
