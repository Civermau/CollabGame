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
		planet node;
		PlanetQuantity++;
		Planet.ID = PlanetQuantity;
		Planet.Name = "Planet-" + PlanetQuantity.ToString();
		if (PlanetQuantity == 2)
		{
            Planet.Position = new Vector2(initPosition.Y + (float)(Math.Cos((1 * 6.283) / (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((1 * 6.282) / (float)PlanetQuantity) * 300));
            GD.Print("Recalculating Planet-1");
            node = GetNode<planet>("Planet-1");
            node.moveTo = (new Vector2(initPosition.Y + (float)(Math.Cos(0) * 300), initPosition.X + (float)(Math.Sin(0) * 300)));
            node.Position = (new Vector2(initPosition.Y + (float)(Math.Cos((0 * 6.283) / (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((0 * 6.282) / (float)PlanetQuantity) * 300)));
            GD.Print(node.Name + " new position : " + node.Position.ToString() + " i = " + 1.ToString() + " Planet Quantity = " + PlanetQuantity.ToString());
            GD.Print("acos(i/PlanetQuantity) = " + Math.Cos(1 / (float)PlanetQuantity).ToString());

        }
		else
		{
			Planet.Position = new Vector2(initPosition.Y + (float)(Math.Cos(0) * 300), initPosition.X + (float)(Math.Sin(0) * 300));

			for (int i = 1; i < PlanetQuantity; i++)
			{
				GD.Print("Recalculating Planet-" + i.ToString());
				node = GetNode<planet>("Planet-" + i.ToString());
				node.moveTo = (new Vector2(initPosition.Y + (float)(Math.Cos((i * 6.283) / (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((i * 6.282) / (float)PlanetQuantity) * 300)));
				//node.Position = (new Vector2(initPosition.Y + (float)(Math.Cos((i * 6.283) / (float)PlanetQuantity) * 300), initPosition.X + (float)(Math.Sin((i * 6.282) / (float)PlanetQuantity) * 300)));
				GD.Print(node.Name + " new position : " + node.Position.ToString() + " i = " + i.ToString() + " Planet Quantity = " + PlanetQuantity.ToString());
				GD.Print("acos(i/PlanetQuantity) = " + Math.Cos(i / (float)PlanetQuantity).ToString());
			}
		}

        Planet.AddPlanet(this);
	}
	public void DestroyPlanet(planet Planet1, planet Planet2)
	{
		PlanetQuantity--;
		int PlanetID = Planet1.ID;
        int random = GD.RandRange(1, 5);
        //AudioStreamPlayer Explosion = CallDeferred("GetNode<AudioStreamPlayer>", ("res://Scripts/Objects/planet.tscn/SFX/Explosion" + random.ToString()));
        //Explosion.Play();

        //el puntero hacia el nodo existe aunque elimines el nodo, por lo que primero tienes que desapuntarlo
        RemoveChild(Planet1);
        Planet1.CallDeferred("DestroyPlanet");
        
        for (int i = PlanetID; i <= PlanetQuantity; i++)
		{
			if (!(GetNodeOrNull<planet>("Planet-" + (i + 1).ToString()) == null))
			{
				planet node = GetNodeOrNull<planet>("Planet-" + (i + 1).ToString());
                GD.Print("aaaaaaa this: " + node.Name);
                RemoveChild(node);
				node.Name = ("Planet-" + i.ToString());
                AddChild(node);
                GD.Print("wtf i: " + i.ToString());
				GD.Print("aaaaaaa Turn into: " + node.Name);

			}

        }
		Planet2.DestroyPlanet();
	}
}
