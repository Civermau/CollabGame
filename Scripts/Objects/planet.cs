using Godot;
using System;

public partial class planet : CharacterBody2D
{

	public const float Speed = 100.0f;
	[Export]
	public bool onSun = false;
	[Export]
	Vector2 direction = new Vector2((float)GD.Randfn(0, 1), (float)GD.Randfn(0, 1));
	public override void _Ready()
	{
		if (GetParentOrNull<PathFollow2D>() != null ) { 
			onSun = true;
			Position = new Vector2(0, 0);
			GD.Print("onSun set true");
		}

	}
	public override void _PhysicsProcess(double delta)
	{
		if(!onSun)
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
		else
		{
			
		}
		
	}

	public void DestroyPlanet()
	{
		this.QueueFree();
	}
	public void AddPlanet(Node node)
	{
		GetParent().RemoveChild(this);
		node.CallDeferred("add_child", this);
	}
}
