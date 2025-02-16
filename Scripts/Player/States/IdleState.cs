// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 19:02
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

public class IdleState : IState
{
    private readonly CharacterBody2D _player;
    private readonly AnimatedSprite2D _animatedSprite2D;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction = Vector2.Down;

    public IdleState(CharacterBody2D player,
        AnimatedSprite2D animatedSprite2D, PlayerFsm fsm)
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
        YumihoshiDebug.Print<IdleState>("玩家状态机", "进入空闲状态");
        _direction = context?.Direction ?? Vector2.Down;
        HandleAnimation();
    }

    public void OnProcess()
    {
        Vector2 direction = GameInputHandler.GetInputDirection();
        if (direction != Vector2.Zero)
        {
            _fsm.Fsm.SwitchState(StateType.PlayerMove);
        }
    }

    public void OnPhysicsProcess()
    {
    }

    public void OnExit(StateContext context = null)
    {
        _animatedSprite2D.Stop();
    }

    /// <summary>
    /// 处理动画
    /// </summary>
    private void HandleAnimation()
    {
        if (_direction == Vector2.Up)
        {
            _animatedSprite2D.Play("IdleUp");
        }
        else if (_direction == Vector2.Down)
        {
            _animatedSprite2D.Play("IdleDown");
        }
        else if (_direction == Vector2.Left)
        {
            _animatedSprite2D.Play("IdleLeft");
        }
        else if (_direction == Vector2.Right)
        {
            _animatedSprite2D.Play("IdleRight");
        }
    }
}
