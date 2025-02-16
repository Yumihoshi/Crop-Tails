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
using LumiVerseFramework.Base.FSM.Types;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player.States;

public class MoveState : IState
{
    private readonly CharacterBody2D _player;
    private readonly AnimatedSprite2D _animatedSprite2D;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction = Vector2.Down;

    public MoveState(CharacterBody2D player, AnimatedSprite2D animatedSprite2D,
        PlayerFsm fsm)
    {
        _player = player;
        _animatedSprite2D = animatedSprite2D;
        _fsm = fsm;
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
        if (newDirection == Vector2.Up)
        {
            _animatedSprite2D.Play("WalkUp");
        }
        else if (newDirection == Vector2.Down)
        {
            _animatedSprite2D.Play("WalkDown");
        }
        else if (newDirection == Vector2.Left)
        {
            _animatedSprite2D.Play("WalkLeft");
        }
        else if (newDirection == Vector2.Right)
        {
            _animatedSprite2D.Play("WalkRight");
        }
        else
        {
            _fsm.Fsm.SwitchState(StateType.PlayerIdle, null, null,
                new StateContext
                {
                    Direction = _direction
                });
        }
        _direction = newDirection;
    }

    public void OnPhysicsProcess()
    {
        _player.Velocity = _direction * _fsm.Speed;
        _player.MoveAndSlide();
    }

    public void OnExit(StateContext context = null)
    {
        _animatedSprite2D.Stop();
    }
}
