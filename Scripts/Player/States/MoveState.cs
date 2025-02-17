// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 20:02
// @version: 1.0
// @description:
// *****************************************************************************

using CropTails.Scripts.Common;
using Godot;
using LumiVerseFramework.Base.FSM;
using LumiVerseFramework.Base.FSM.Interfaces;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player.States;

public class MoveState : IState
{
    private readonly CharacterBody2D _player;
    private readonly AnimationTree _animationTree;
    private readonly AnimationPlayer _animationPlayer;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction = Vector2.Down;

    public MoveState(CharacterBody2D player,
        AnimationTree animationTree,
        AnimationPlayer animationPlayer,
        PlayerFsm fsm)
    {
        _player = player;
        _fsm = fsm;
        _animationPlayer = animationPlayer;
        _animationTree = animationTree;
    }

    public bool OnCheck(StateContext context = null)
    {
        return true;
    }

    public void OnEnter(StateContext context = null)
    {
        YumihoshiDebug.Print<MoveState>("玩家状态机", "进入移动状态");
    }

    public void OnProcess()
    {
        Vector2 newDirection = GameInputHandler.GetInputDirection();
        if (newDirection != Vector2.Zero)
        {
            _animationTree.Set("parameters/Walk/blend_position", newDirection);
            _animationTree.Set("parameters/conditions/Move", true);
        }
        else
        {
            _fsm.Fsm.SwitchState(PlayerStateType.Idle, null, null,
                new StateContext
                {
                    Direction = _direction
                });
        }

        _direction = newDirection;
    }

    public void OnPhysicsProcess()
    {
        _player.Velocity = _direction.Normalized() * _fsm.Speed;
        _player.MoveAndSlide();
    }

    public void OnExit(StateContext context = null)
    {
        _animationPlayer.Stop();
        _animationTree.Set("parameters/conditions/Move", false);
    }
}
