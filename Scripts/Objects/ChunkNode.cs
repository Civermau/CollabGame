using Godot;
using System;
using System.IO.Pipes;

public partial class ChunkNode : Node
{

	[Export]
	public int x, y;
	public override void _Ready()
	{
		GetNode<Area2D>("Area2D").Position = new Vector2(x * 1536, y * 1536);

		PackedScene planet = ResourceLoader.Load<PackedScene>("res://Scripts/Objects/planet.tscn");
		PackedScene meteor = ResourceLoader.Load<PackedScene>("res://Scripts/Objects/meteor.tscn");

		meteor Meteor = (meteor)meteor.Instantiate();
		planet Planet = (planet)planet.Instantiate();
        
		GD.Randomize();
		int chances = GD.RandRange(0, 19);
		if (chances <= 2)
		{
            Vector2 position = new Vector2(GD.RandRange(-750, 750), GD.RandRange(-750, 750));
            Planet.Position = new Vector2(x * 1536, y * 1536) + position;
            AddChild(Planet);
		}
		if (chances <= 6)
		{
            Vector2 position = new Vector2(GD.RandRange(-750, 750), GD.RandRange(-750, 750));
            Meteor.Position = new Vector2(x * 1536, y * 1536) + position;
            AddChild(Meteor);
		}
    }

    public override void _Process(double delta)
    {
        if(Name != ("ChunkNode" + x.ToString() + "," + y.ToString()))
		{
			QueueFree();
		}
    }

    private void _on_area_2d_body_entered(Node2D body)
	{
        if (body is sun sun){
			sun Sun = (sun)body;
			for(int i = -2; i < 3; i++)
			{
				for(int j = -2; j < 3; j++)
				{
					if (GetParent().GetNodeOrNull<ChunkNode>("ChunkNode" + (x + i).ToString() + "," + (y + j).ToString()) == null && !(i == -2 || i == 2 || j == -2 || j == 2)) {
						PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scripts/Objects/ChunkNode.tscn");
						ChunkNode node = (ChunkNode)scene.Instantiate();
						node.Name = "ChunkNode" + (x + i).ToString() + "," + (y + j).ToString();
						node.x = x + i;
						node.y = y + j;
                        GetParent().CallDeferred("add_child", node);
                    }
					if (i == -2 || i == 2 || j == -2 || j == 2)
					{
						if (GetParent().GetNodeOrNull<ChunkNode>("ChunkNode" + (x + i).ToString() + "," + (y + j).ToString()) != null)
						{
                            GetParent().GetNode<ChunkNode>("ChunkNode" + (x + i).ToString() + "," + (y + j).ToString()).QueueFree();

                        }
					}
				}
			}



		}
	}
	//hello
}
