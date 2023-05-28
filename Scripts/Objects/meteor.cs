using Godot;
using System;

public partial class meteor : CharacterBody2D
{
    private Vector2 direction = new Vector2(GD.Randf(), GD.Randf());
    private const float Speed = 6000.0f;

	private Vector2 sizeMultiplier;
	private int velocityMultiplier;

    public override void _Ready()
	{
		GD.Print(this.Name + " succesfully loaded!");

		GD.Randomize();
		AnimatedSprite2D meteorSprite = GetNode<AnimatedSprite2D>("Sprites");


        int rand = GD.RandRange(-10, 10);
		string animID = (Math.Abs(rand) < 10 ? "0" + Mathf.Abs(rand).ToString() : Mathf.Abs(rand).ToString());
		meteorSprite.Animation = ("Meteor" + animID);
		meteorSprite.Play();
		meteorSprite.SpeedScale = rand;	

		float randSize = GD.Randi() % 2 + 1 + GD.Randf();
		sizeMultiplier = new Vector2(randSize, randSize);
		this.Scale = this.Scale * sizeMultiplier;
	}

	public override void _Process(double delta)
	{
        Velocity = direction * Speed * (float)delta;
        MoveAndSlide();
	}

	public void destroyMeteor()
	{
        int random = GD.RandRange(1, 5);
        AudioStreamPlayer Explosion = GetNode<AudioStreamPlayer>("SFX/Explosion" + random);
        Explosion.Play();
    }
}
