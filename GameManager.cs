using Godot;

public partial class GameManager : Node2D
{
    private int _score;
    private bool _isGameOver;

    private static GameManager _instance = new();

    public static GameManager Instance => _instance;

    public bool IsGameOver => _isGameOver;

    public int Score
    {
        get => _score; set
        {
            _score = value;
            EmitSignal(nameof(ScoreUpdated), _score);
            GD.Print($"Score: {_score}");
        }
    }

    [Signal]
    public delegate void ScoreUpdatedEventHandler(int score);

    public void GameOver()
    {
        GD.Print("Game over!");
        _isGameOver = true;
    }
}
