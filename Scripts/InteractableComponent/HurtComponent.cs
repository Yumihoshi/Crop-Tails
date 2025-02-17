using CropTails.Scripts.Globals;
using Godot;

namespace CropTails.Scripts.InteractableComponent;

public partial class HurtComponent : Area2D
{
    [Export] public ToolType Tool { get; set; } = ToolType.None;
    
    [Signal]
    public delegate void OnHurtEventHandler(int damage);

    private void _OnAreaEntered(Area2D area)
    {
        if (area is not HitComponent hitComponent) return;
        if (Tool == hitComponent.CurrentTool)
        {
            EmitSignal(SignalName.OnHurt, hitComponent.HitDamage);
        }
    }
}
