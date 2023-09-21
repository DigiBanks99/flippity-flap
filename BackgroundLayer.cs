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

        GameManager.Instance.GameOverChanged += OnGameOver;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    protected override void Dispose(bool disposing)
    {
        GameManager.Instance.GameOverChanged -= OnGameOver;

        base.Dispose(disposing);
    }

    private void OnGameOver(bool isGameOver)
    {
        _cloudParticles.Emitting = !isGameOver;
        _cloudParticles.SpeedScale = isGameOver ? 0f : 1f;
    }
}
