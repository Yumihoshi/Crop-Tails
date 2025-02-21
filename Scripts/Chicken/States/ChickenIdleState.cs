// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/21 18:02
// @version: 1.0
// @description:
// *****************************************************************************

using Godot;
using LumiVerseFramework.Base.FSM;
using LumiVerseFramework.Base.FSM.Interfaces;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Chicken.States;

public class ChickenIdleState : IState
{
    private readonly CharacterBody2D _character;
    private readonly AnimatedSprite2D _animatedSprite;
    private readonly Timer _timer = new();
    private readonly float _idleInterval;
    private readonly ChickenFsm _fsm;
    private bool _idleStateTimeout = false;

    public ChickenIdleState(CharacterBody2D character,
        AnimatedSprite2D animatedSprite, float idleInterval, ChickenFsm fsm)
    {
        _character = character;
        _animatedSprite = animatedSprite;
        _idleInterval = idleInterval;
        _fsm = fsm;
    }

    public bool OnCheck(StateContext context = null)
    {
        _timer.WaitTime = _idleInterval;
        _timer.Timeout += OnTimerTimeout;
        _fsm.AddChild(_timer);
        return true;
    }

    public void OnEnter(StateContext context = null)
    {
        YumihoshiDebug.Print<ChickenIdleState>("鸡状态机", "进入Idle状态");
        _idleStateTimeout = false;
        _timer.Start();
        _animatedSprite.Play("Idle");
    }

    public void OnProcess()
    {
        if (!_idleStateTimeout) return;
        _fsm.Fsm.SwitchState(ChickenStateType.Walk);
    }

    public void OnPhysicsProcess()
    {
    }

    public void OnExit(StateContext context = null)
    {
        _animatedSprite.Stop();
        _timer.Stop();
        _timer.Timeout -= OnTimerTimeout;
    }

    /// <summary>
    /// 计时器超时
    /// </summary>
    private void OnTimerTimeout()
    {
        _idleStateTimeout = true;
    }
}
