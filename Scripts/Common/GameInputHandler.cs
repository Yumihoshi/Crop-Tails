// *****************************************************************************
// @author: 绘星tsuki
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/02/16 20:02
// @version: 1.0
// @description:
// *****************************************************************************

using Godot;

namespace CropTails.Scripts.Common;

public static class GameInputHandler
{
    private static Vector2 _direction = Vector2.Zero;

    /// <summary>
    /// 获取移动方向
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetInputDirection()
    {
        if (Input.IsActionPressed("WalkUp"))
        {
            _direction = Vector2.Up;
        }
        else if (Input.IsActionPressed("WalkDown"))
        {
            _direction = Vector2.Down;
        }
        else if (Input.IsActionPressed("WalkLeft"))
        {
            _direction = Vector2.Left;
        }
        else if (Input.IsActionPressed("WalkRight"))
        {
            _direction = Vector2.Right;
        }
        else
        {
            _direction = Vector2.Zero;
        }

        return _direction;
    }

    /// <summary>
    /// 是否有移动输入
    /// </summary>
    /// <returns></returns>
    public static bool IsMovementInput()
    {
        return _direction != Vector2.Zero;
    }

    /// <summary>
    /// 是否使用工具
    /// </summary>
    /// <returns></returns>
    public static bool UseTool()
    {
        return Input.IsActionJustPressed("Hit") || Input.IsActionPressed("Hit");
    }
}
