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
    [ExportGroup("依赖节点")] [Export] private AnimationTree _animationTree;

    [Export] private AnimationPlayer _animationPlayer;
    [ExportGroup("属性配置")] [Export] public int Speed { get; set; } = 100;
    private Player _playerNode;
    private CharacterBody2D _player;

    public override void _Ready()
    {
        base._Ready();
        _playerNode = GetNode<Player>("..");
        _player = GetNode<CharacterBody2D>("..");
        Fsm.AddState(PlayerStateType.Idle,
            new IdleState(_player, _animationTree, _animationPlayer,
                this));
        Fsm.AddState(PlayerStateType.Move,
            new MoveState(_player, _animationTree, _animationPlayer,
                this));
        Fsm.AddState(PlayerStateType.Chop,
            new ChopState(_playerNode, _animationTree,
                _animationPlayer, this));
        Fsm.AddState(PlayerStateType.Till,
            new TillState(_playerNode, _animationTree,
                _animationPlayer, this));
        Fsm.AddState(PlayerStateType.Water,
            new WaterState(_playerNode, _animationTree,
                _animationPlayer, this));
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

    private void SwitchIdleState(Vector2 direction)
    {
        Fsm.SwitchState(PlayerStateType.Idle, null, null, new StateContext
        {
            Direction = direction
        });
    }
}
