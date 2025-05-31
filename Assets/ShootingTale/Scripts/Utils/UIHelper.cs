using UnityEngine;
using Scene.Menu;

public static class UIHelper
{
    public static (RectTransform, Vector2) MoveTarget(RectTransform target, Vector2 position)
    {
        return (target, position);
    }

    public static bool IsTransitionLocked(this MenuType type)
    {
        return type is MenuType.Confirmation;
    }

    public static bool IsMenuFrozen(this MenuType type)
    {
        return type is MenuType.Main or MenuType.Setting or MenuType.Other;
    }

    public static bool IsAllLocked(this MenuState state)
    {
        return state is MenuState.Selection;
    }
}