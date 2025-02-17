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
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player;

public partial class PlayerFsm : FsmNode<PlayerStateType>
{
    [ExportGroup("状态配置")] [Export] private CharacterBody2D _player;
    [Export] private AnimatedSprite2D _animatedSprite2D;
    [Export] public int Speed { get; set; } = 100;
    private Player _playerNode;

    public override void _Ready()
    {
        base._Ready();
        _playerNode = GetNode<Player>("..");
        Fsm.AddState(PlayerStateType.Idle,
            new IdleState(_player, _animatedSprite2D, this));
        Fsm.AddState(PlayerStateType.Move,
            new MoveState(_player, _animatedSprite2D, this));
        Fsm.AddState(PlayerStateType.Chop,
            new ChopState(_playerNode, _animatedSprite2D, this));
        Fsm.AddState(PlayerStateType.Till,
            new TillState(_playerNode, _animatedSprite2D, this));
        Fsm.AddState(PlayerStateType.Water,
            new WaterState(_playerNode, _animatedSprite2D, this));
        Fsm.SwitchState(PlayerStateType.Idle);
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
