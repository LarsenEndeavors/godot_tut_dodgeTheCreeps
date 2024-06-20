using Godot;
using static DebugHelper;

public partial class Main : Node
{
    [Export]
    public PackedScene MobScene { get; set; }

    private int _score;

    public void GameOver()
    {
        DebugPrint("GameOver()");
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    public void NewGame()
    {
        DebugPrint("NewGame()");
        _score = 0;
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
        GetNode<Label>("Message").Text = "";
        GetNode<Hud>("Hud").UpdateScore(_score);
        GetNode<Timer>("StartTimer").Start();
    }

    public void OnStartTimerTimeout()
    {
        DebugPrint("OnStartTimerTimeout()");
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }
    
    public void OnScoreTimerTimeout()
    {
        DebugPrint("OnScoreTimerTimeout()");
        _score++;
        GetNode<Hud>("Hud").UpdateScore(_score);
    }

    public void OnMobTimerTimeout()
    {
        DebugPrint("OnMobTimerTimeout()");

        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        var mob = MobScene.Instantiate<Mob>();
        
        // Choose a random location on Path2D
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        
        // Set the mob's position to a random location
        mob.Position = mobSpawnLocation.Position;
        
        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        
        // Add some randomness to the direction
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);
        
        // Spawn the mob by adding it to the Main Scene
        AddChild(mob);
        DebugPrint($"Spawned Mob at {mob.Position} with direction {mob.Rotation}");
    }

    public override void _Ready()
    {
    }

}
