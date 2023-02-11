using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor Gui class responsible for drawing alignment values to editor displays. 
/// </summary>
public static class NLin_EGUIDrawer_Alignment
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

    //General use data. 
    private const int GUI_REMOVEBUTTON_WIDTH = 120;
    private const string GUI_REMOVEBUTTOM_TEXT = "Remove Alignment";

    public static void Draw(ref NLin_XML_PropAlignmentData data, string[] options, out bool remove)
    {
        GUILayout.BeginHorizontal();

        //Draw alignment name and identifier.
        NLin_HelperFunctions.DrawID(data.identifier, GUI_ROOM_ALIGNMENT_ID_LENGTH);
        data.identifier = EditorGUILayout.Popup("", data.identifier, options, GUILayout.Width(GUI_ROOM_ALIGNMENT_IDENTIFIER));

        //Draw alignment effect value. 
        data.effectValue = EditorGUILayout.FloatField(data.effectValue, GUILayout.Width(60));

        //Draw the alignment removal button. 
        remove = GUILayout.Button(GUI_REMOVEBUTTOM_TEXT, GUILayout.Width(GUI_REMOVEBUTTON_WIDTH));

        GUILayout.EndHorizontal(); 
    }

    /// <summary>
    /// Draws a room alignment in editor GUI. 
    /// </summary>
    /// <param name="data"> A reference to the alignment to draw (required for setting data).</param>
    /// <param name="options"> The assignable alignment options.</param>
    /// <param name="remove"> Whether the user has indicated that the alignment should be removed.</param>
    public static void Draw(ref NLin_XML_Alignment data, string[] options, out bool remove)
    {
        NLin_XML_Range rangeRef = data.matchRange; 
        GUILayout.BeginHorizontal();

        //Draw Alignment Name and Identifier.
        NLin_HelperFunctions.DrawID(data.identifier, GUI_ROOM_ALIGNMENT_ID_LENGTH);
        data.identifier = EditorGUILayout.Popup("", data.identifier, options, GUILayout.Width(GUI_ROOM_ALIGNMENT_IDENTIFIER));
        GUILayout.Space(GUI_ROOM_SPACE_1);

        //Draw Alignment Match/Threshold Values.
        NLin_EdGuiDrawer_Range.Draw(ref rangeRef, GUI_ROOM_THRESHOLD_WIDTH);
        rangeRef = data.thresholdRange;
        NLin_EdGuiDrawer_Range.Draw(ref rangeRef, GUI_ROOM_THRESHOLD_WIDTH);

        //Draw the alignment removal button. 
        remove = GUILayout.Button(GUI_REMOVEBUTTOM_TEXT, GUILayout.Width(GUI_REMOVEBUTTON_WIDTH));

        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Draws an alignment in editor GUI. 
    /// </summary>
    /// <param name="alignment"> The alignment to display. </param>
    /// <param name="remove"> Whether the user has opted to remove the alignment. </param>
    public static void Draw(ref NLin_XML_AlignmentDef alignment, out bool remove)
    {
        NLin_XML_Range rangeRef = alignment.range;

        GUILayout.BeginHorizontal();

        //Draw Alignment Name and Identifier. 
        alignment.name = GUILayout.TextField(alignment.name, GUILayout.Width(GUI_ALIGNMENT_NAME_WIDTH));
        GUILayout.Label(" (ID: " + alignment.identifier + ")", GUILayout.Width(GUI_ALIGNMENT_IDENTIFIER_WIDTH));

        //Draw Alignment Value. 
        GUILayout.Space(GUI_ALIGNMENT_SPACE1);
        alignment.value = EditorGUILayout.FloatField(alignment.value, GUILayout.Width(GUI_ALIGNMENT_RANGE_WIDTH));
        GUILayout.Space(GUI_ALIGNMENT_SPACE2);

        //Draw the min and max cap range.
        NLin_EdGuiDrawer_Range.Draw(ref rangeRef, GUI_ALIGNMENT_RANGE_WIDTH);

        //Draw the alignment removal button. 
        remove = GUILayout.Button(GUI_REMOVEBUTTOM_TEXT, GUILayout.Width(GUI_REMOVEBUTTON_WIDTH));

        GUILayout.EndHorizontal();
    }

}