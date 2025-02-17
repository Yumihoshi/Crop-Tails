// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 19:02
// @version: 1.0
// @description:
// *****************************************************************************

using System;
using CropTails.Scripts.Common;
using CropTails.Scripts.Globals;
using Godot;
using LumiVerseFramework.Base.FSM;
using LumiVerseFramework.Base.FSM.Interfaces;
using LumiVerseFramework.Common;

namespace CropTails.Scripts.Player.States;

public class IdleState : IState
{
    private readonly CharacterBody2D _player;
    private readonly AnimatedSprite2D _animatedSprite2D;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction = Vector2.Down;
    private Vector2 _lastDirection = Vector2.Down;

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
        _lastDirection = _direction;
        HandleAnimation();
    }

    public void OnProcess()
    {
        // 如果有输入，则切换到移动状态
        Vector2 direction = GameInputHandler.GetInputDirection();
        if (direction != Vector2.Zero)
        {
            _lastDirection = direction;
            _fsm.Fsm.SwitchState(PlayerStateType.Move);
            return;
        }

        // 使用工具
        if (GameInputHandler.UseTool())
        {
            ToolType curTool = _player.GetNode<Player>(".").CurrentTool;
            switch (curTool)
            {
                case ToolType.None:
                    break;
                case ToolType.AxeWood:
                    _fsm.Fsm.SwitchState(PlayerStateType.Chop, null, null,
                        new StateContext
                        {
                            Direction = _lastDirection
                        });
                    return;
                case ToolType.TillGround:
                    _fsm.Fsm.SwitchState(PlayerStateType.Till, null, null,
                        new StateContext
                        {
                            Direction = _lastDirection
                        });
                    return;
                case ToolType.WaterCrops:
                    _fsm.Fsm.SwitchState(PlayerStateType.Water, null, null,
                        new StateContext
                        {
                            Direction = _lastDirection
                        });
                    return;
                case ToolType.PlantCorn:
                    break;
                case ToolType.PlantTomato:
                    break;
                default:
                    YumihoshiDebug.Error<IdleState>("玩家状态机", "未知工具类型");
                    return;
            }
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
