using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class NLin_HelperFunctions
{
    public static void PrintChapter(Chapter chapter)
    {
        Debug.Log(
            string.Format(
                "Level: <name: {0}, ID: {1}, type: {2}>", 
                chapter.Title,
                chapter.ID, 
                chapter.Type
                )
            );
    }

    #region Data Build Functions.

    #endregion

    #region GUI Helpers. 
    public static void DrawLabelAndID(string title, int identifier)
    {
        GUILayout.Label(title + " (ID: " + identifier + "): ", GUILayout.Width(150));
    }

    public static void DrawID(int identifier, int width)
    {
        GUILayout.Label(" (ID: " + identifier + "): ", GUILayout.Width(width));
    }

    public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }

    #endregion

    #region Comparison Functions. 

    #endregion
}
