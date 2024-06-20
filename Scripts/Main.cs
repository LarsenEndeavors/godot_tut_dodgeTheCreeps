using Godot;
public partial class Main : Node
{
    [Export]
    public PackedScene MobScene { get; set; }

    private int _score;

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<Hud>("HUD").ShowGameOver();
        GetNode<Hud>("HUD").UpdateStartButtonText("Restart");
    }

    public void NewGame()
    {
        _score = 0;
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
        var hud = GetNode<Hud>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");
        GetNode<Timer>("StartTimer").Start();
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void OnScoreTimerTimeout()
    {
        _score++;
        GetNode<Hud>("HUD").UpdateScore(_score);
    }

    public void OnMobTimerTimeout()
    {

        if (MobScene == null)
        {
            GD.Print("MobScene is not set. Cannot instantiate mob.");
            return;
        }

        var mob = MobScene.Instantiate<Mob>();
        if (mob == null)
        {
            GD.Print("Failed to instantiate Mob.");
            return;
        }

        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        mob.Position = mobSpawnLocation.Position;

        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }

    public override void _Ready()
    {
        if (MobScene == null)
        {
            GD.Print("MobScene is not loaded. Please assign the MobScene in the Godot editor or load it manually.");
        }
    }

}
