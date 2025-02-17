using System.Collections;
using System.Threading.Tasks;
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
        // 修改材质shader，实现摇晃
        Material.Set("shader_parameter/ShakeIntensify", 0.5f);
        _ = ResetMaterial();
    }

    private async Task ResetMaterial()
    {
        await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
        Material.Set("shader_parameter/ShakeIntensify", 0.0f);
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
