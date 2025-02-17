// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/17 14:02
// @version: 1.0
// @description:
// *****************************************************************************

using CropTails.Scripts.Globals;
using Godot;

namespace CropTails.Scripts.Player;

public partial class Player : CharacterBody2D
{
    [ExportGroup("当前工具")]
    [Export]
    public ToolType CurrentTool { get; set; } = ToolType.None;
}
