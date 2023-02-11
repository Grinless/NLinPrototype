using UnityEditor;
using UnityEngine;

/// <summary>
/// Script allowing the creating of a range control in editor. 
/// </summary>
public static class NLin_EdGuiDrawer_Range
{
    /// <summary>
    /// Draw a range in editor. 
    /// </summary>
    /// <param name="data">   The data instance to draw.          </param>
    /// <param name="width">  The width of each range float box.  </param>
    /// <param name="space1"> The space after each float control. </param>
    public static void Draw(ref NLin_XML_Range data, int width, int space1 = 30)
    {
        data.min = EditorGUILayout.FloatField(data.min, GUILayout.Width(width));
        data.max = EditorGUILayout.FloatField(data.max, GUILayout.Width(width));
        GUILayout.Space(space1);
    }
}
