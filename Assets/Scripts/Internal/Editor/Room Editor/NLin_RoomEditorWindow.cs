using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class NLin_RoomEditorWindow : EditorWindow
{
    NLin_XML_BiomeTree rTree;
    NLin_XML_AlignmentDefTree aTree;
    string[] alignmentOptions;
    string[] roomTypeOptions;
    bool newTree, loadTree, saveTree, addRoom;
    public static NLin_XML_Biome selectedForEdit;

    [MenuItem("NLin/Rooms/Room Editor")]
    public static void ShowEditor()
    {
        NLin_RoomEditorWindow window = GetWindow<NLin_RoomEditorWindow>();
        window.titleContent = new GUIContent("Room Editor");
    }

    #region Inbuilt Functions. 

    private void OnEnable()
    {
        alignmentOptions = NLin_EditorManager.AlignmentDefTreeSTRArray;
        roomTypeOptions = NLin_EditorManager.CurrentNodeTypesSTRArray;

        NLin_EditorManager.AlignmentsChanged += OnAlignmentChange; 
    }

    private void OnDisable()
    {
        NLin_EditorManager.AlignmentsChanged -= OnAlignmentChange;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUIDraw();
        GUILayout.EndVertical();
    }

    #endregion

    private void OnAlignmentChange()
    {
        //Get the new data into the editor. 
        alignmentOptions = NLin_EditorManager.AlignmentDefTreeSTRArray;
        roomTypeOptions = NLin_EditorManager.CurrentNodeTypesSTRArray;
    }

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
        NLin_XML_Biome dataSel; //Reference container. 

        //Draw the toolbar and handle results. 
        ToolbarDrawAndResolve();

        //Check if the data is loaded, return if not loaded. 
        if (!CanUpdate())
            return;

        //Else draw rooms. 
        foreach (NLin_XML_Biome item in rTree.rooms)
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
            rTree = new NLin_XML_BiomeTree();
            AddRoom();
        }

        if (loadTree)
            rTree = NLin_XML_Serialization.Deserialize<NLin_XML_BiomeTree>(XMLFileNames.roomTreeFilename);

        if (saveTree)
            NLin_XML_Serialization.Serialize<NLin_XML_BiomeTree>(rTree, XMLFileNames.roomTreeFilename);

        if (addRoom)
            AddRoom();
    }

    #endregion

    #region Room Toolbar.

    private void DrawAndResolveRoomToolbar(ref NLin_XML_Biome data)
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
                data.roomAlignments = new List<NLin_XML_Alignment>();
                data.AddAlignment();
            }

            NLin_RoomEditor.CreateWizard();
        }

        if (addAlignment)
        {
            if (data.roomAlignments == null)
            {
                data.roomAlignments = new List<NLin_XML_Alignment>();
            }
            data.AddAlignment();
        }
    }

    #endregion

    #region Draw Rooms
    private void DrawRoom(ref NLin_XML_Biome data)
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

    /// <summary>
    /// Draws the room alignment header information. 
    /// </summary>
    private void DrawRoomAlignmentsHeader()
    {
        //Draw Label Row 1 
        GUILayout.BeginHorizontal();
        GUILayout.Label("Alignments: ", GUILayout.Width(100));
        GUILayout.Space(100);
        GUILayout.Label(" | Match Range: ", GUILayout.Width(150));
        GUILayout.Label(" | Threshold Range: ");
        GUILayout.EndHorizontal();

        //Draw second
        GUILayout.BeginHorizontal();
        GUILayout.Space(230);
        GUILayout.Label("Min", GUILayout.Width(55));
        GUILayout.Label("Max", GUILayout.Width(90));
        GUILayout.Label("Min", GUILayout.Width(50));
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
    private void DrawAlignments(ref NLin_XML_Biome data)
    {
        NLin_XML_Alignment alignment;
        List<NLin_XML_Alignment> alignmentsToRemove = new List<NLin_XML_Alignment>();
        GUILayout.BeginVertical();
        foreach (NLin_XML_Alignment roomAlignment in data.roomAlignments)
        {
            alignment = roomAlignment;
            bool remove;
            NLin_EGUIDrawer_Alignment.Draw(ref alignment, alignmentOptions, out remove);
            if (remove)
                alignmentsToRemove.Add(alignment);
        }
        GUILayout.EndVertical();

        data.RemoveAlignments(alignmentsToRemove);
    }

    #endregion
    #endregion
}

public class NLin_RoomEditor : ScriptableWizard
{
    public string roomName = NLin_RoomEditorWindow.selectedForEdit.name;
    public NLin_XML_NodeType roomType = NLin_RoomEditorWindow.selectedForEdit.roomType;

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