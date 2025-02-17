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
    private readonly AnimationTree _animationTree;
    private readonly AnimationPlayer _animationPlayer;
    private readonly PlayerFsm _fsm;
    private Vector2 _direction;

    public WaterState(Player player, AnimationTree animationTree,
        AnimationPlayer animationPlayer,
        PlayerFsm fsm)
    {
        _player = player;
        _animationPlayer = animationPlayer;
        _animationTree = animationTree;
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
        _animationTree.Set("parameters/Water/blend_position", _direction);
        _animationTree.Set("parameters/conditions/Water", true);
    }

    public void OnProcess()
    {
        
    }

    public void OnPhysicsProcess()
    {
    }

    public void OnExit(StateContext context = null)
    {
        _animationTree.Set("parameters/conditions/Water", false);
    }
}
