using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class containing helpful functions for NLin. 
/// </summary>
public static class NLin_HelperFunctions
{
    /// <summary>
    /// Print the details of a chapter to Unity output editor. 
    /// </summary>
    /// <param name="chapter"> The chapter to print. </param>
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
    /// <summary>
    /// Handy GUI function for drawing a label ID pair. 
    /// </summary>
    /// <param name="title"> The title to draw. </param>
    /// <param name="identifier"> The identifier to draw. </param>
    public static void DrawLabelAndID(string title, int identifier)
    {
        GUILayout.Label(title + " (ID: " + identifier + "): ", GUILayout.Width(150));
    }

    /// <summary>
    /// Draw an identifier (GUI).
    /// </summary>
    /// <param name="identifier"> The identifier to display. </param>
    /// <param name="width"> The width of the label. </param>
    public static void DrawID(int identifier, int width)
    {
        GUILayout.Label(" (ID: " + identifier + "): ", GUILayout.Width(width));
    }

    /// <summary>
    /// Draw a GUI indentation line. 
    /// </summary>
    /// <param name="color"> The color the line should be draw as. </param>
    /// <param name="thickness"> The thickness of the line when drawn. </param>
    /// <param name="padding"> The area of space around the line. </param>
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