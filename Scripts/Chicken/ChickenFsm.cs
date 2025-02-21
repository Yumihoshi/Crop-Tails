// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/21 18:02
// @version: 1.0
// @description:
// *****************************************************************************

using CropTails.Scripts.Chicken.States;
using Godot;
using LumiVerseFramework.Base.FSM;

namespace CropTails.Scripts.Chicken;

public partial class ChickenFsm : FsmNode<ChickenStateType>
{
    [ExportGroup("Idle状态")] [Export] private CharacterBody2D _character;
    [Export] private AnimatedSprite2D _animatedSprite;
    [Export] private float _idleInterval = 5f;

    public override void _Ready()
    {
        base._Ready();
        // 初始化状态机
        Fsm.AddState(ChickenStateType.Idle,
            new ChickenIdleState(_character, _animatedSprite, _idleInterval,
                this));
        Fsm.AddState(ChickenStateType.Walk,
            new ChickenWalkState(_character, _animatedSprite));
        Fsm.SwitchState(ChickenStateType.Idle);
    }
}
