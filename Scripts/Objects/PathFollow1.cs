using Godot;
using System;

public partial class PathFollow1 : PathFollow2D
{
	[Export]
	public const double velocity = .05;
	public int PlanetQuantity = 0;
	public Vector2 initPosition;
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
        Planet.ID = PlanetQuantity;
		
		Planet.Name = "Planet-" + PlanetQuantity.ToString();
        PlanetQuantity++;
        Planet.Position = new Vector2(initPosition.Y + (float)(Math.Cos((Planet.ID * 6.283) / (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((Planet.ID * 6.282) / (float)PlanetQuantity) * 300));
		Planet.onSun = true;
        Planet.AddPlanet(this);
	}
    public void DestroyPlanet(planet Planet1, planet Planet2)
    {
        PlanetQuantity--;
		Planet1.DestroyPlanet();
        Planet2.DestroyPlanet();
    }
	public void _on_planet_out_detector_body_exited(Node2D body)
	{
		if(body is planet planet)
		{
			planet Planet = (planet)body;
			if (!Planet.onSun)
			{
				Planet.DestroyPlanet();
			}
		}
		if(body is meteor meteor)
		{
			meteor Meteor = (meteor)body;
			Meteor.QueueFree();
		}
	}
}
