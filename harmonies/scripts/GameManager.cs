using Godot;
using System;

public partial class GameManager : Node
{
	public int score = 0;
	public int maxScore = 4;
	private Node2D _endScreen;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_endScreen = GetNode<Node2D>("%end_screen");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void AddPoint()
	{
		score += 1;

		if (score == maxScore) 
		{
			var tween = CreateTween();
			tween.TweenProperty(_endScreen, "modulate", Colors.White, 2.0f);
		}
	}
}
