using Godot;

public partial class QuitButton : Button
{
    public void OnButtonPressed()
    {
        GetTree().Quit();
    }
}
