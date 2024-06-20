using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	public void ClearMessage()
	{
		var message = GetNode<Label>("Message");
		message.Text = "";
		message.Hide();
	}
	
	public void ShowMessage(string text, float timeOut = 0)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
		GetNode<Timer>("MessageTimer").Start();
	}

	public async void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);
		ShowMessage("Dodge\nthe\nCreeps!", -1);

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreValue").Text = score.ToString();
	}

	public void UpdateStartButtonText(string text)
	{
		GetNode<Button>("StartButton").Text = text;
	}

	public void OnStartButtonPressed()
	{
		ClearMessage();
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout()
	{
		ClearMessage();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
