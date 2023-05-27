using Godot;
using System;

public partial class ChunkNode : Node
{

	[Export]
	public int x, y;
	public override void _Ready()
	{
		//PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scripts/Objects/planet.tscn");
		//planet node = (planet)scene.Instantiate();
		//AddChild(node);
		GetNode<Area2D>("Area2D").Position = new Vector2(x * 1536, y * 1536);
        

    }

	private void _on_area_2d_body_entered(Node2D body)
	{
        GD.Print("works1");
        if (body is sun sun){
			GD.Print("works");
			sun Sun = (sun)body;
			for(int i = -2; i < 3; i++)
			{
				for(int j = -2; j < 3; j++)
				{
					if (GetParent().GetNodeOrNull<ChunkNode>("ChunkNode" + (x + i).ToString() + "," + (y + j).ToString()) == null && !(i == -2 || i == 2 || j == -2 || j == 2)) {
						GD.Print("get node returns null on ChunkNode" + (x + i).ToString() + "," + (y + j).ToString());
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

	private void _on_area_2d_body_exited(Node2D body)
	{
		if (body is sun sun){
			sun Sun = (sun)body;
			GD.Print("shit");
		}
	}

}
