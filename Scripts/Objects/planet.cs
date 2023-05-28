using Godot;
using System;

public partial class planet : CharacterBody2D
{

	public const float Speed = 6000.0f;
	[Export]
	public bool onSun = false;
	public bool onSunEntered = false;
	[Export]
	public Vector2 direction = new Vector2((float)GD.Randfn(0, 1), (float)GD.Randfn(0, 1));
	public Vector2 moveTo;
	public int ID;
	public override void _Ready()
	{
		GD.Randomize();
		if (GetParentOrNull<PathFollow2D>() != null ) { 
			onSun = true;
			Position = new Vector2(0, 0);
			GD.Print("onSun set true");
		}
		moveTo = Position;
		ID = -1;
	}
	public override void _PhysicsProcess(double delta)
	{
		if(onSunEntered == false && onSun == true)
		{
			moveTo = Position;
			onSunEntered = true;
		}
		if(!onSun)
		{

			Vector2 velocity = Velocity;

			if (direction != Vector2.Zero)
			{
				velocity = direction * Speed * (float)delta;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * (float)delta);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
		else
		{
			if(!(Position.X - moveTo.X < 1 && Position.Y - moveTo.Y < 1 && Position.X - moveTo.X > -1 && Position.Y - moveTo.Y > -1))
			{
				Position = new Vector2(Position.X + ((moveTo.X - Position.X) * (float)delta), Position.Y + ((moveTo.Y - Position.Y) * (float)delta));
			}
		}
		
	}

	public void DestroyPlanet()
	{ 

		this.QueueFree();
	}
	public void AddPlanet(Node node)
	{
		onSun = true;
		GetParent().RemoveChild(this);
		node.CallDeferred("add_child", this, true);
	}
	public void UpdatePosition(Vector2 NewPosition)
	{
		moveTo = NewPosition;
	}

	private void _on_planet_detector_body_entered(Node2D body)
	{
		if (body is planet planet) {
			planet Planet = (planet)body;
			if (Planet.ID != ID && !Planet.onSun)
			{
				GD.Print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				if (GetParentOrNull<PathFollow1>() != null)
				{
					GetParentOrNull<PathFollow1>().DestroyPlanet(this, Planet);

				}
			}
		}
	}
	public void NewName(string newName)
	{
		Name = newName;
	}
}
