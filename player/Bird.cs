using FlippityFlap;
using Godot;

public partial class Bird : CharacterBody2D
{
    private float _defaultGravity;

    [Export]
    public float FlapForce { get; set; }
    [Export]
    public float Mass { get; set; } = 1f;
    [Export]
    public float GravityScale { get; set; } = 1f;
    [Export]
    public bool CanMove { get; set; } = true;
    [Export]
    public bool IsDead { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        _defaultGravity = GodotUtilities.ProjectSettingsExtended.GetSettingOrDefault<float>("physics/2d/default_gravity");
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        ApplyGravity(delta);
        HandleInput(delta);

        MoveAndSlide();
    }

    public void Kill()
    {
        IsDead = true;
        GD.Print("Player died");
        GameManager.Instance.GameOver();
    }

    public void OnPipeDetectorBodyEntered(Node2D body)
    {
        Kill();
    }

    private void ApplyGravity(double delta)
    {
        if (!CanMove)
        {
            return;
        }

        Velocity = Velocity with
        {
            Y = Velocity.Y + Mass * (float)delta * _defaultGravity * GravityScale
        };
    }

    private void HandleInput(double delta)
    {
        if (Input.IsActionPressed(InputActionMap.Flap))
        {
            if (!CanFlap())
            {
                return;
            }

            Velocity = Velocity with
            {
                Y = Velocity.Y - FlapForce * (float)delta * Input.GetActionStrength(InputActionMap.Flap) * _defaultGravity * GravityScale
            };
        }
    }

    private bool CanFlap()
    {
        return CanMove && !IsDead;
    }
}
