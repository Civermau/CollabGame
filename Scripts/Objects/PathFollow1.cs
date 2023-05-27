using Godot;
using System;

public partial class PathFollow1 : PathFollow2D
{
	[Export]
	public const double velocity = .05;
	private int PlanetQuantity = 0;
	Vector2 initPosition;
	public override void _Ready()
	{
		initPosition = Position;
	}

	public override void _Process(double delta)
	{
		ProgressRatio += (float)(velocity * delta);
	}

	public void _on_planet_detector_body_entered(Node2D body)
	{
		if (body is planet planet)
		{
			planet Planet = (planet)body;
			if (!Planet.onSun)
			{
				CreatePlanet(Planet);
			}

			GD.Print(Planet.Name + " ha entrado al area de deteccion");
		}
	}
	public void CreatePlanet(planet Planet)
	{
		Node2D node;
		PlanetQuantity++;
		Planet.onSun = true;
		Planet.Name = "Planet" + PlanetQuantity.ToString();
		Planet.Position = new Vector2(initPosition.Y + (float)(Math.Cos(0) * 300), initPosition.X + (float)(Math.Sin(0) * 300));
		for (int i = 1; i < PlanetQuantity; i++)
		{
			GD.Print("Recalculating Planet" + i.ToString());
			node = GetNode<Node2D>("Planet" + i.ToString());
			node.Position = new Vector2(initPosition.Y + (float)(Math.Cos((i * 6.283)/ (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((i * 6.282) / (float)PlanetQuantity) * 300));
			GD.Print(node.Name + " new position : " + node.Position.ToString() + " i = " + i.ToString() + " Planet Quantity = " + PlanetQuantity.ToString());
			GD.Print("acos(i/PlanetQuantity) = " + Math.Cos(i / (float)PlanetQuantity).ToString());
		}
		Planet.AddPlanet(this);
	}
}
