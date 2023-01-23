using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class NLin_RoomEditorWindow : EditorWindow
{
    XML_RoomTree rTree;
    XML_AlignmentsTree aTree;
    string[] alignmentOptions;
    string[] roomTypeOptions;
    bool newTree, loadTree, saveTree, addRoom;
    public static XML_RoomData selectedForEdit;

    [MenuItem("NLin/Rooms/Room Editor")]
    public static void ShowEditor()
    {
        NLin_RoomEditorWindow window = GetWindow<NLin_RoomEditorWindow>();
        window.titleContent = new GUIContent("Room Editor");
    }

    #region Inbuilt Functions. 

    private void OnEnable()
    {
        if (aTree == null)
        {
            LoadAlignments();
        }
        LoadRoomTypes();
    }


    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUIDraw();
        GUILayout.EndVertical();
    }

    #endregion

    #region Data Loading. 
    private void LoadAlignments()
    {
        aTree = XMLSerialization.Deserialize<XML_AlignmentsTree>(XMLFileNames.atfilename);
        List<string> temp = new List<string>();
        //Deserialize Alignment Types. 
        for (int i = 0; i < aTree.alignments.Count; i++)
        {
            temp.Add(aTree.alignments[i].name);
        }
        alignmentOptions = temp.ToArray();
    }

    private void LoadRoomTypes()
    {
        //Get room types as an array. 
        roomTypeOptions = Enum.GetNames(typeof(RoomTypeEnum));
    }
    #endregion

    #region Utility Checks. 

    private bool CanUpdate()
    {
        if (rTree == null)
        {
            return false;
        }

        if (rTree.rooms.Count <= 0)
        {
            return false;
        }

        return true;
    }

    #endregion

    #region GUI Drawing. 

    private void GUIDraw()
    {
        XML_RoomData dataSel; //Reference container. 

        //Draw the toolbar and handle results. 
        ToolbarDrawAndResolve();

        //Check if the data is loaded, return if not loaded. 
        if (!CanUpdate())
            return;

        //Else draw rooms. 
        foreach (XML_RoomData item in rTree.rooms)
        {
            dataSel = item;
            DrawRoom(ref dataSel);
        }
    }

    #region Main Toolbar. 

    private void ToolbarDrawAndResolve()
    {
        GUILayout.BeginHorizontal();
        newTree = GUILayout.Button("New Rooms data.");
        loadTree = GUILayout.Button("Load Rooms Data.");
        saveTree = GUILayout.Button("Save Rooms Data.");
        addRoom = GUILayout.Button("Add Room.");
        GUILayout.EndHorizontal();
        ResolveOptions();
    }

    private void ResolveOptions()
    {
        if (newTree)
        {
            rTree = new XML_RoomTree();
            AddRoom();
        }

        if (loadTree)
            rTree = XMLSerialization.Deserialize<XML_RoomTree>(XMLFileNames.rtfilename);

        if (saveTree)
            XMLSerialization.Serialize<XML_RoomTree>(rTree, XMLFileNames.rtfilename);

        if (addRoom)
            AddRoom();
    }

    #endregion

    #region Room Toolbar.
    
    private void DrawAndResolveRoomToolbar(ref XML_RoomData data)
    {
        GUILayout.BeginHorizontal();
        bool editRoom = GUILayout.Button("Edit Room.", new GUILayoutOption[] { GUILayout.Width(80) });
        bool addAlignment = GUILayout.Button("Add Alignment.", new GUILayoutOption[] { GUILayout.Width(100) });
        GUILayout.EndHorizontal();

        if (editRoom)
        {
            selectedForEdit = data;
            if (data.roomAlignments == null)
            {
                data.roomAlignments = new List<XML_RoomAlignment>();
                data.AddAlignment();
            }

            NLin_RoomEditor.CreateWizard();
        }

        if (addAlignment)
        {
            if (data.roomAlignments == null)
            {
                data.roomAlignments = new List<XML_RoomAlignment>();
            }
            data.AddAlignment();
        }
    }

    #endregion

    #region Draw Rooms
    private void DrawRoom(ref XML_RoomData data)
    {
        //int selected = 0;
        EditorGUILayout.Separator();
        NLin_HelperFunctions.DrawUILine(Color.black);
        GUILayout.BeginHorizontal();
        NLin_HelperFunctions.DrawLabelAndID(data.name, data.identifier);
        GUILayout.Label("Room Type: ", new GUILayoutOption[] { GUILayout.Width(100) });
        int selection = EditorGUILayout.Popup(((int)data.roomType), roomTypeOptions.ToArray());
        data.ResolveAndSetRoomType(selection);
        GUILayout.EndHorizontal();
        NLin_HelperFunctions.DrawUILine(Color.grey);
        DrawRoomAlignmentsHeader();
        DrawAlignments(ref data);
        DrawAndResolveRoomToolbar(ref data);
    }

    private void DrawRoomAlignmentsHeader()
    {
        //Draw Label Row 1 
        GUILayout.BeginHorizontal();
        GUILayout.Label("Alignments: ", GUILayout.Width(100));
        GUILayout.Space(120);
        GUILayout.Label(" | Match Range: ");
        GUILayout.Space(20);
        GUILayout.Label(" | Threshold Range: ");
        GUILayout.EndHorizontal();

        //Draw second
        GUILayout.BeginHorizontal();
        GUILayout.Space(260);
        GUILayout.Label("Min", GUILayout.Width(75));
        GUILayout.Space(2);
        GUILayout.Label("Max");
        GUILayout.Space(40);
        GUILayout.Label("Min", GUILayout.Width(80));
        GUILayout.Space(5);
        GUILayout.Label("Max");
        GUILayout.EndHorizontal();
    }

    private void AddRoom() => rTree.AddRoom();

    #endregion

    #region Draw Room Alignments. 

    /// <summary>
    /// Draw the list of alignments within a room. 
    /// </summary>
    /// <param name="data">The XML RoomData</param>
    private void DrawAlignments(ref XML_RoomData data)
    {
        XML_RoomAlignment alignment;
        GUILayout.BeginVertical();
        foreach (XML_RoomAlignment roomAlignment in data.roomAlignments)
        {
            alignment = roomAlignment;
            DrawAlignment(ref alignment);
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// Draw the room alignment. 
    /// </summary>
    /// <param name="data"> The XML_RoomAlignment reference to draw. </param>
    private void DrawAlignment(ref XML_RoomAlignment alignment)
    {
        GUILayout.BeginHorizontal();
        //Draw Alignment Name and Identifier.
        NLin_HelperFunctions.DrawID(alignment.identifier, 40);
        alignment.identifier = EditorGUILayout.Popup("", alignment.identifier, alignmentOptions.ToArray(), GUILayout.Width(100));
        GUILayout.Space(80);
        //Draw Alignment Match threshold.
        alignment.matchMin = EditorGUILayout.FloatField(alignment.matchMin, GUILayout.Width(80));
        alignment.matchMax = EditorGUILayout.FloatField(alignment.matchMax, GUILayout.Width(80));
        GUILayout.Space(80);
        alignment.thresholdMin = EditorGUILayout.FloatField(alignment.thresholdMin, GUILayout.Width(80));
        alignment.thresholdMax = EditorGUILayout.FloatField(alignment.thresholdMax, GUILayout.Width(80));
        GUILayout.EndHorizontal();
    }

    #endregion
    #endregion
}

public class NLin_RoomEditor : ScriptableWizard
{
    public string roomName = NLin_RoomEditorWindow.selectedForEdit.name;
    public RoomTypeEnum roomType = NLin_RoomEditorWindow.selectedForEdit.roomType;
    
    public static void CreateWizard()
    {
        //Generate new wizard. 
        ScriptableWizard.DisplayWizard<NLin_RoomEditor>("Room Alignment Editor", "Update", "Cancel");
    }

    private void OnWizardCreate()
    {
        NLin_RoomEditorWindow.selectedForEdit.name = roomName;
        NLin_RoomEditorWindow.selectedForEdit.roomType = roomType;
    }

    private void OnWizardOtherButton() => this.Close();
}