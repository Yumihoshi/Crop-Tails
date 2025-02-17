using System.Threading.Tasks;
using CropTails.Scripts.InteractableComponent;
using Godot;

namespace CropTails.Scripts.Objects;

public partial class Rock : Sprite2D
{
    [ExportGroup("依赖节点")] [Export] private HurtComponent _hurtComponent;
    [Export] private DamageComponent _damageComponent;
    private PackedScene _stoneScene;

    public override void _Ready()
    {
        base._Ready();
        _hurtComponent.OnHurt += HandleOnHurt;
        _damageComponent.OnMaxDamageReached += OnMaxDamageReached;
        _stoneScene =
            ResourceLoader.Load<PackedScene>(
                "res://Scenes/Objects/Rock/Stone.tscn");
    }

    private void HandleOnHurt(int hitDamage)
    {
        _damageComponent.ApplyDamage(hitDamage);// 修改材质shader，实现摇晃
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
        CallDeferred(nameof(AddStoneScene));
        QueueFree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        _hurtComponent.OnHurt -= HandleOnHurt;
        _damageComponent.OnMaxDamageReached -= OnMaxDamageReached;
    }

    private void AddStoneScene()
    {
        Node2D stone = _stoneScene.Instantiate<Node2D>();
        stone.GlobalPosition = GlobalPosition;
        GetParent().AddChild(stone);
        Tween tween = stone.CreateTween();
        tween.TweenProperty(stone, "position:y", stone.Position.Y - 30, 0.3f);
        tween.TweenProperty(stone, "position:y", GlobalPosition.Y, 0.3f);
    }
}