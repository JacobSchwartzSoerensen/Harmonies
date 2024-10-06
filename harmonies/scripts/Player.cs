using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void FluteEventHandler(string note);

	private float _speed = 400.0f;
	private float _jumpSpeed = -1500.0f;
	
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 3;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("flute_1"))
		{
			_PlayFluteNote("flute_1");
		}
		if (Input.IsActionJustPressed("flute_2"))
		{
			_PlayFluteNote("flute_2");
		}
		if (Input.IsActionJustPressed("flute_3"))
		{
			_PlayFluteNote("flute_3");
		}
		if (Input.IsActionJustPressed("flute_4"))
		{
			_PlayFluteNote("flute_4");
		}
		if (Input.IsActionJustPressed("flute_5"))
		{
			_PlayFluteNote("flute_5");
		}
		if (Input.IsActionJustPressed("flute_6"))
		{
			_PlayFluteNote("flute_6");
		}
		if (Input.IsActionJustPressed("flute_7"))
		{
			_PlayFluteNote("flute_7");
		}
		if (Input.IsActionJustPressed("flute_8"))
		{
			_PlayFluteNote("flute_8");
		}
		if (Input.IsActionJustPressed("flute_9"))
		{
			_PlayFluteNote("flute_9");
		}
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

	private void _PlayFluteNote(string note)
	{
		var flute = GetNode<AudioStreamPlayer2D>(note);

		if (note == null) return;

		flute.Play();

		EmitSignal(SignalName.Flute, note);
	}
}
