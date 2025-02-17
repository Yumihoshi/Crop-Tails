using Godot;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Door;

public partial class Door : StaticBody2D
{
    [ExportGroup("组件配置")]
    [Export] private AnimatedSprite2D _animatedSprite2D;
    [Export] private CollisionShape2D _collisionShape2D;
    private InteractableComponent.InteractableComponent _interactableComponent;
    
    public override void _Ready()
    {
        base._Ready();
        _interactableComponent =
            GetNode<InteractableComponent.InteractableComponent>(
                "InteractableComponent");
        _interactableComponent.InteractableActivated += OnInteractableActivated;
        _interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
    }

    private void OnInteractableActivated()
    {
        _collisionShape2D.SetDeferred("disabled", true);
        _animatedSprite2D.Play("OpenDoor");
    }

    private void OnInteractableDeactivated()
    {
        _collisionShape2D.SetDeferred("disabled", false);
        _animatedSprite2D.Play("CloseDoor");
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _interactableComponent.InteractableActivated -= OnInteractableActivated;
        _interactableComponent.InteractableDeactivated -= OnInteractableDeactivated;
    }
}
