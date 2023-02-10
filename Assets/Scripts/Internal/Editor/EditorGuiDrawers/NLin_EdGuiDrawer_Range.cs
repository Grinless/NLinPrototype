using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class NLin_EdGuiDrawer_Range
{
    public static void Draw(ref NLin_XML_Range data, int width, int space1 = 30)
    {
        data.min = EditorGUILayout.FloatField(data.min, GUILayout.Width(width));
        data.max = EditorGUILayout.FloatField(data.max, GUILayout.Width(width));
        GUILayout.Space(space1);
    }
}
