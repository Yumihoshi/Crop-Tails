using Godot;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.InteractableComponent;

public partial class InteractableComponent : Area2D
{
    [Signal]
    public delegate void InteractableActivatedEventHandler();

    [Signal]
    public delegate void InteractableDeactivatedEventHandler();

    private void _OnBodyEntered(Node2D body)
    {
        EmitSignal(SignalName.InteractableActivated);
    }

    private void _OnBodyExited(Node2D body)
    {
        EmitSignal(SignalName.InteractableDeactivated);
    }
}
