// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 15:02
// @version: 1.0
// @description:
// *****************************************************************************

using LumiVerseFramework.Base.FSM.Interfaces;
using LumiVerseFramework.Common;

namespace LumiVerseFramework.Base.FSM.States;

public class TestState : IState
{
    public bool OnCheck(StateContext context = null)
    {
        YumihoshiDebug.Print<TestState>("TestState", "OnCheck成功");
        return true;
    }

    public void OnEnter(StateContext context = null)
    {
        YumihoshiDebug.Print<TestState>("TestState", "OnEnter成功");
    }

    public void OnProcess()
    {
        YumihoshiDebug.Print<TestState>("TestState", "OnProcess成功");
    }

    public void OnPhysicsProcess()
    {
        YumihoshiDebug.Print<TestState>("TestState", "OnPhysicsProcess成功");
    }

    public void OnExit(StateContext context = null)
    {
        YumihoshiDebug.Print<TestState>("TestState", "OnExit成功");
    }
}
