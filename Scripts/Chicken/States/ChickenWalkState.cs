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

public class ChickenWalkState : IState
{
    private CharacterBody2D _character;
    private AnimatedSprite2D _animatedSprite;

    public ChickenWalkState(CharacterBody2D character,
        AnimatedSprite2D animatedSprite)
    {
        _character = character;
        _animatedSprite = animatedSprite;
    }

    public bool OnCheck(StateContext context = null)
    {
        return true;
    }

    public void OnEnter(StateContext context = null)
    {
        YumihoshiDebug.Print<ChickenWalkState>("鸡状态机", "进入Walk状态");
        _animatedSprite.Play("Walk");
    }

    public void OnProcess()
    {
    }

    public void OnPhysicsProcess()
    {
    }

    public void OnExit(StateContext context = null)
    {
        _animatedSprite.Stop();
    }
}
