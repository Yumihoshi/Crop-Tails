// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/17 15:02
// @version: 1.0
// @description:
// *****************************************************************************

using Godot;
using LumiVerseFramework.Base.FSM;
using LumiVerseFramework.Base.FSM.Interfaces;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player.States;

public class WaterState : IState
{
    private readonly Player _player;
    private readonly AnimatedSprite2D _animatedSprite2D;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction;

    public WaterState(Player player, AnimatedSprite2D animatedSprite2D,
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
        YumihoshiDebug.Print<ChopState>("玩家状态机", "进入浇水状态");
        _direction = context.Direction;
        if (_direction == Vector2.Up)
        {
            _animatedSprite2D.Play("WaterUp");
        }
        else if (_direction == Vector2.Down)
        {
            _animatedSprite2D.Play("WaterDown");
        }
        else if (_direction == Vector2.Left)
        {
            _animatedSprite2D.Play("WaterLeft");
        }
        else if (_direction == Vector2.Right)
        {
            _animatedSprite2D.Play("WaterRight");
        }
    }

    public void OnProcess()
    {
        if (!_animatedSprite2D.IsPlaying())
        {
            _fsm.Fsm.SwitchState(PlayerStateType.Idle, null, null,
                new StateContext
                {
                    Direction = _direction
                });
        }
    }

    public void OnPhysicsProcess()
    {
    }

    public void OnExit(StateContext context = null)
    {
        _animatedSprite2D.Stop();
    }
}
