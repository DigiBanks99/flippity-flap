using Godot;
using GodotUtilities;

[Scene]
public partial class Hud : Control
{
    [Node("PauseOverlay/MessageLabel")]
    private Label _messageLabel;
    private bool _paused;
    [Node]
    private ColorRect _pauseOverlay;
    [Node("PauseOverlay/Menu/ResumeButton")]
    private Button _resumeButton;
    private SceneTree _sceneTree;
    [Node]
    private Label _scoreLabel;

    public bool Paused
    {
        get => _paused; set
        {
            _paused = value;
            _sceneTree.Paused = _paused;
            _pauseOverlay.Visible = _paused;
            _messageLabel.Text = "Paused";
        }
    }

    public override void _Ready()
    {
        base._Ready();

        _sceneTree = GetTree();
        WireNodes();

        GameManager.Instance.ScoreUpdated += OnScoreUpdated;
        GameManager.Instance.GameOverChanged += OnGameOver;

        UpdateScoreText(0);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause") && !GameManager.Instance.IsGameOver)
        {
            Paused = !Paused;
            GetViewport().SetInputAsHandled(); // stops propogation
        }
    }

    protected override void Dispose(bool disposing)
    {
        GameManager.Instance.ScoreUpdated -= OnScoreUpdated;
        GameManager.Instance.GameOverChanged -= OnGameOver;

        base.Dispose(disposing);
    }

    public void OnResumePressed()
    {
        Paused = false;
    }

    private void OnGameOver(bool isGameOver)
    {
        Paused = true;
        _messageLabel.Text = "You've died...";
        _resumeButton.Visible = false;
    }

    private void OnScoreUpdated(int score)
    {
        UpdateScoreText(score);
    }

    private void UpdateScoreText(int score)
    {
        _scoreLabel.Text = $"Score: {score}";
    }
}
