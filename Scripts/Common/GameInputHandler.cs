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
        _direction = Vector2.Zero;
        if (Input.IsActionPressed("WalkLeft"))
        {
            _direction.X = -1;
        }
        if (Input.IsActionPressed("WalkRight"))
        {
            _direction.X = 1;
        }
        if (Input.IsActionPressed("WalkUp"))
        {
            _direction.Y = -1;
        }
        if (Input.IsActionPressed("WalkDown"))
        {
            _direction.Y = 1;
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
        return Input.IsActionJustPressed("Hit");
    }
}
