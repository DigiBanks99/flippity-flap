using Godot;
using GodotUtilities;

[Scene]
public partial class BackgroundLayer : CanvasLayer
{
    [Node]
    private GpuParticles2D _cloudParticles;

    public override void _Ready()
    {
        base._Ready();

        WireNodes();

        GameManager.Instance.GameOverChanged += OnGameOverChanged;
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        _cloudParticles.Emitting = !isGameOver;
        _cloudParticles.SpeedScale = isGameOver ? 0f : 1f;
    }
}
