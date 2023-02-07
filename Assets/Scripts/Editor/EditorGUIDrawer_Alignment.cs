using UnityEngine;
using UnityEditor;

public static class EditorGUIDrawer_Alignment
{
    //Alignment constants. 
    private const int GUI_ALIGNMENT_NAME_WIDTH = 100;
    private const int GUI_ALIGNMENT_IDENTIFIER_WIDTH = 50;
    private const int GUI_ALIGNMENT_RANGE_WIDTH = 50;
    private const int GUI_ALIGNMENT_SPACE1 = 30;
    private const int GUI_ALIGNMENT_SPACE2 = 60;  

    //Room Alignment constants. 
    private const int GUI_ROOM_ALIGNMENT_ID_LENGTH = 40;
    private const int GUI_ROOM_ALIGNMENT_IDENTIFIER = 100;
    private const int GUI_ROOM_THRESHOLD_WIDTH = 60;
    private const int GUI_ROOM_SPACE_1 = 60;
    private const int GUI_ROOM_SPACE_2 = 30;
    private const int GUI_ROOM_SPACE_3 = 40;

    //General use data. 
    private const int GUI_REMOVEBUTTON_WIDTH = 120;
    private const string GUI_REMOVEBUTTOM_TEXT = "Remove Alignment";


    /// <summary>
    /// Draws a room alignment in editor gui. 
    /// </summary>
    /// <param name="alignment"> A reference to the alignment to draw (required for setting data).</param>
    /// <param name="options"> The assignable alignment options.</param>
    /// <param name="removeAlignment"> Whether the user has indicated that the alignment should be removed.</param>
    public static void Draw(ref NLin_XML_RoomAlignment alignment, string[] options, out bool removeAlignment)
    {
        GUILayout.BeginHorizontal();

        //Draw Alignment Name and Identifier.
        NLin_HelperFunctions.DrawID(alignment.identifier, GUI_ROOM_ALIGNMENT_ID_LENGTH);
        alignment.identifier = EditorGUILayout.Popup("", alignment.identifier, options, GUILayout.Width(GUI_ROOM_ALIGNMENT_IDENTIFIER));
        GUILayout.Space(GUI_ROOM_SPACE_1);
        
        //Draw Alignment Match Values.
        alignment.matchMin = EditorGUILayout.FloatField(alignment.matchMin, GUILayout.Width(GUI_ROOM_THRESHOLD_WIDTH));
        alignment.matchMax = EditorGUILayout.FloatField(alignment.matchMax, GUILayout.Width(GUI_ROOM_THRESHOLD_WIDTH));
        GUILayout.Space(GUI_ROOM_SPACE_2);
        
        //Draw Alignment Threshold Values.
        alignment.thresholdMin = EditorGUILayout.FloatField(alignment.thresholdMin, GUILayout.Width(GUI_ROOM_THRESHOLD_WIDTH));
        alignment.thresholdMax = EditorGUILayout.FloatField(alignment.thresholdMax, GUILayout.Width(GUI_ROOM_THRESHOLD_WIDTH));
        GUILayout.Space(GUI_ROOM_SPACE_3);
        
        //Draw the alignment removal button. 
        removeAlignment = GUILayout.Button(GUI_REMOVEBUTTOM_TEXT, GUILayout.Width(GUI_REMOVEBUTTON_WIDTH));
        
        GUILayout.EndHorizontal();
    }

    public static void Draw(ref NLin_XML_Alignment alignment, out bool remove)
    {
        GUILayout.BeginHorizontal();
        
        //Draw Alignment Name and Identifier. 
        alignment.name = GUILayout.TextField(alignment.name, GUILayout.Width(GUI_ALIGNMENT_NAME_WIDTH));
        GUILayout.Label(" (ID: " + alignment.identifier + ")", GUILayout.Width(GUI_ALIGNMENT_IDENTIFIER_WIDTH));

        //Draw Alignment Value. 
        GUILayout.Space(GUI_ALIGNMENT_SPACE1);
        alignment.initalValue = EditorGUILayout.FloatField(alignment.initalValue, GUILayout.Width(GUI_ALIGNMENT_RANGE_WIDTH));
        GUILayout.Space(GUI_ALIGNMENT_SPACE2);
        
        //Draw the min and max cap range. 
        alignment.valueMinimumCap = EditorGUILayout.FloatField(alignment.valueMinimumCap, GUILayout.Width(GUI_ALIGNMENT_RANGE_WIDTH));
        alignment.valueMaximumCap = EditorGUILayout.FloatField(alignment.valueMaximumCap, GUILayout.Width(GUI_ALIGNMENT_RANGE_WIDTH));
        GUILayout.Space(GUI_ALIGNMENT_SPACE2);

        //Draw the alignment removal button. 
        remove = GUILayout.Button(GUI_REMOVEBUTTOM_TEXT, GUILayout.Width(GUI_REMOVEBUTTON_WIDTH));
        
        GUILayout.EndHorizontal();
    }

}
