using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DebugHelper.DebugPrint("Mob._Ready()");
		var animatedSprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var mobTypes = animatedSprite2d.SpriteFrames.GetAnimationNames();
		animatedSprite2d.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		DebugHelper.DebugPrint("OnVisibleOnScreenNotifier2DScreenExited()");
		QueueFree();
	}
}
