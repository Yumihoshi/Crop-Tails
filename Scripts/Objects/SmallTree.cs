using CropTails.Scripts.InteractableComponent;
using Godot;

namespace CropTails.Scripts.Objects;

public partial class SmallTree : Sprite2D
{
    [ExportGroup("依赖节点")] [Export] private HurtComponent _hurtComponent;
    [Export] private DamageComponent _damageComponent;
    private PackedScene _logScene;

    public override void _Ready()
    {
        base._Ready();
        _hurtComponent.OnHurt += HandleOnHurt;
        _damageComponent.OnMaxDamageReached += OnMaxDamageReached;
        _logScene =
            ResourceLoader.Load<PackedScene>(
                "res://Scenes/Objects/Tree/Log.tscn");
    }

    private void HandleOnHurt(int hitDamage)
    {
        _damageComponent.ApplyDamage(hitDamage);
    }

    private void OnMaxDamageReached()
    {
        CallDeferred(nameof(AddLogScene));
        QueueFree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _hurtComponent.OnHurt -= HandleOnHurt;
        _damageComponent.OnMaxDamageReached -= OnMaxDamageReached;
    }

    private void AddLogScene()
    {
        Node2D log = _logScene.Instantiate<Node2D>();
        log.GlobalPosition = GlobalPosition;
        GetParent().AddChild(log);
        Tween tween = log.CreateTween();
        tween.TweenProperty(log, "position:y", log.Position.Y - 30, 0.3f);
        tween.TweenProperty(log, "position:y", GlobalPosition.Y, 0.3f);
    }
}
