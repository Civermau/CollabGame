using Godot;
using System;

public partial class mainCode : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_timer_timeout()
	{
        PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scripts/Objects/planet.tscn");
        planet node = (planet)scene.Instantiate();
        AddChild(node);
    }
}
