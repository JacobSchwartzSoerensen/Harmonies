using Godot;
using System;
using System.Threading.Tasks;

public partial class Stone : Area2D
{
	[Export]
	public string Melody { get; set; } = "";

	private string _recordedMelody = "";
	private bool on;
	private Sprite2D onSprite;
	private Sprite2D offSprite;
	private GameManager gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		onSprite = GetNode<Sprite2D>("on");
		offSprite = GetNode<Sprite2D>("off");
		gameManager = GetNode<GameManager>("%GameManager");

		onSprite.Modulate = new Color(1, 1, 1, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.Flute += _OnFlutePlayed;
			var notes = GetNode<AudioStreamPlayer2D>("notes");

			if (notes != null && !on)
			{
				notes.Play();
			}
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			player.Flute -= _OnFlutePlayed;
		}
	}

	private void _OnFlutePlayed(string note)
	{
		if (on) return;

		var noteNr = note.Substr(6, 1);
		_recordedMelody += noteNr;
		
		if (_recordedMelody.Length >= 3)
		{
			var latestMelody = _recordedMelody.Substring(_recordedMelody.Length - 3);

			GD.Print(latestMelody);

			if (latestMelody == Melody)
			{
				on = true;
				_OnCorrectMelody();
			}
		}
	}

	private async Task _OnCorrectMelody()
	{
		gameManager.AddPoint();

		await ToSignal(GetTree().CreateTimer(1.5), "timeout");

		var tween = CreateTween();
		tween.TweenProperty(onSprite, "modulate", Colors.White, 1.0f);

		var chord = GetNode<AudioStreamPlayer2D>("chord");

		if (chord != null) chord.Play();
	}
}
