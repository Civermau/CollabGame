using Godot;
using System;

public partial class planet_ui : Control
{
	[Export(PropertyHint.Flags)]
	private bool isMenuShown = false;
	public override void _Ready()
	{
		GD.Print("UI succesfully loaded");
		
	}

	public override void _Process(double delta)
	{
	}

	private void _on_upgrades_pressed()
	{
		if (!isMenuShown)	
		{
			GetNode<AnimationPlayer>("Animations").Play("ShowHide-UpgMenu");
		} else
		{
            GetNode<AnimationPlayer>("Animations").PlayBackwards("ShowHide-UpgMenu");
        }
	}
}
