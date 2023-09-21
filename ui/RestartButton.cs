using Godot;
using GodotUtilities;

[Scene]
public partial class RestartButton : Button
{
    private SceneTree _sceneTree;

    public override void _Ready()
    {
        base._Ready();

        _sceneTree = GetTree();
    }

    public void OnButtonPressed()
    {
        GameManager.Instance.Reset();

        _sceneTree.Paused = false;
        _sceneTree.ReloadCurrentScene();
    }
}
