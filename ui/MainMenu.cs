using Godot;
using System.Collections.Generic;

[Tool]
public partial class MainMenu : Control
{
    [Export]
    public PackedScene NextScene { get; set; }

    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = new();

        if (NextScene is null)
        {
            warnings.Add("The Next Scene must be set so starting the game navigates to the correct scene");
        }

        return warnings.ToArray();
    }

    public void OnPlayButtonPressed()
    {
        if (NextScene is null)
        {
            return;
        }

        GetTree().ChangeSceneToPacked(NextScene);
    }
}
