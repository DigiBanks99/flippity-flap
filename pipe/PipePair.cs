using Godot;

public partial class PipePair : Node2D
{
    [Export]
    public float Speed { get; set; } = 300;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        Position = new Vector2(Position.X - (float)delta * Speed, Position.Y);
    }

    public void OnScreenExited()
    {
        GD.Print($"Disposing pipe pair: {Name}");
        QueueFree();
    }

    public void OnGapBodyEntered(Node2D body)
    {
        GameManager.Instance.Score++;
    }
}
