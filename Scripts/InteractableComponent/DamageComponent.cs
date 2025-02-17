using Godot;

namespace CropTails.Scripts.InteractableComponent;

public partial class DamageComponent : Node2D
{
    [ExportGroup("属性配置")] [Export] public int MaxDamage { get; set; } = 1;
    [Export] public int CurrentDamage { get; set; }
    
    [Signal]
    public delegate void OnMaxDamageReachedEventHandler();

    public void ApplyDamage(int damage)
    {
        CurrentDamage = Mathf.Clamp(CurrentDamage + damage, 0, MaxDamage);
        if (CurrentDamage == MaxDamage)
        {
            EmitSignal(SignalName.OnMaxDamageReached);
        }
    }
}
