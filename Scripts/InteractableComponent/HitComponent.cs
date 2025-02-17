using CropTails.Scripts.Globals;
using Godot;

namespace CropTails.Scripts.InteractableComponent;

public partial class HitComponent : Area2D
{
    [Export] public ToolType CurrentTool { get; set; } = ToolType.None;
    [Export] public int HitDamage { get; set; } = 1;
}