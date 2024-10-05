using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float _speed = 400.0f;
	private float _jumpSpeed = -1500.0f;
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 3;
	
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		velocity.Y += Gravity * (float)delta;

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = _jumpSpeed;
		}

		var dir = Input.GetAxis("move_left", "move_right");
		velocity.X = dir * _speed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
