using Godot;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.InteractableComponent;

public partial class CollectableComponent : Area2D
{
    [Export] public string CollectableName { get; set; }

    private void _OnBodyEntered(Node2D body)
    {
        if (body is not Player.Player) return;
        YumihoshiDebug.Print<CollectableComponent>("可收集物", "已被收集");
        GetParent().QueueFree();
    }
}