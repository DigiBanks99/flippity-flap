using Godot;
using GodotUtilities;
using System.Collections.Generic;

[Tool, Scene]
public partial class PipePairSpawner : Node2D
{
    [Export]
    public float SpawnPointX { get; set; }
    [Export]
    public PackedScene PipePair { get; set; }

    [Node]
    public Timer _spawnTimer;
    private int _pipePairCounter;

    public override void _Ready()
    {
        base._Ready();

        GameManager.Instance.GameOverChanged += OnGameOver;

        WireNodes();
    }


    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = new();

        if (PipePair is null)
        {
            warnings.Add("The Pipe Pair needs to be set");
        }

        return warnings.ToArray();
    }

    public void OnSpawnTimerTimeout()
    {
        SpawnPipePair();
    }

    private void SpawnPipePair()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        Node2D pipePair = PipePair.Instantiate() as Node2D;
        pipePair.Name = $"{nameof(PipePair)}{_pipePairCounter++}";
        pipePair.Position = new Vector2(SpawnPointX, GD.RandRange(100, 400));

        AddChild(pipePair);
    }

    private void OnGameOver(bool isGameOver)
    {
        if (isGameOver)
        {
            _spawnTimer.Stop();
        }
        else
        {
            _spawnTimer.Start();
        }
    }
}
