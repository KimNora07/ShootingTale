using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyItemColor
{
    static HierarchyItemColor()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null || !go.name.StartsWith("/", StringComparison.Ordinal)) return;
        EditorGUI.DrawRect(selectionRect, new Color(0.2196078431372549f, 0.2196078431372549f, 0.2196078431372549f, 1f));
        EditorGUI.DropShadowLabel(selectionRect, go.name.Replace("/", "").ToUpperInvariant());
    }
}