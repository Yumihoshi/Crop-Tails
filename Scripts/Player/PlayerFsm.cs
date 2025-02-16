// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 19:02
// @version: 1.0
// @description:
// *****************************************************************************

using CropTails.Scripts.Player.States;
using Godot;
using LumiVerseFramework.Base.FSM;
using LumiVerseFramework.Base.FSM.Types;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player;

public partial class PlayerFsm : FsmNode
{
    [ExportGroup("状态配置")] [Export] private CharacterBody2D _player;
    [Export] private AnimatedSprite2D _animatedSprite2D;
    [Export] public int Speed { get; set; } = 100;

    public override void _Ready()
    {
        base._Ready();
        Fsm.AddState(StateType.PlayerIdle,
            new IdleState(_player, _animatedSprite2D, this));
        Fsm.AddState(StateType.PlayerMove,
            new MoveState(_player, _animatedSprite2D, this));
        Fsm.SwitchState(StateType.PlayerIdle);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("SwitchFullscreen"))
        {
            YumihoshiFullScreen.SwitchFullScreenAuto();
        }
    }
}
