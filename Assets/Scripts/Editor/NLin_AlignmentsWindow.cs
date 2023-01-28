using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

/// <summary>
/// Used to assist in the creation of alignments used by N_Lin. 
/// </summary>
public class NLin_AlignmentsWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    Rect headerSectionRect = new Rect() { x = 0, y = 0, width = Screen.width, height = 20 };
    Rect displaySectionRect = new Rect() { x = 0, y = 20.1f, height = 500 };
    Texture2D headerTexture;
    Texture2D displayTexture;

    XML_AlignmentsTree tree = new XML_AlignmentsTree();
    public static XML_Alignment selected;

    [MenuItem("NLin/Alignments/AlignmentEditor")]
    public static void ShowExample()
    {
        NLin_AlignmentsWindow wnd = GetWindow<NLin_AlignmentsWindow>();
        wnd.titleContent = new GUIContent("AlignmentsEditor");
    }

    public void OnEnable()
    {
        InitTextures();
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawAlignmentDisplay();
    }

    public void InitTextures()
    {
        InitHeaderTexture();
        InitDisplayTexture();
    }

    public void DrawLayouts()
    {
        headerSectionRect.width = Screen.width;
        GUI.DrawTexture(headerSectionRect, headerTexture);

        displaySectionRect.width = Screen.width;
        GUI.DrawTexture(displaySectionRect, displayTexture);
    }

    #region Header Functions.
    private void InitHeaderTexture()
    {
        headerTexture = new Texture2D(1, 1);
        headerTexture.SetPixel(0, 0, Color.gray);
        headerTexture.Apply();
    }

    private void DrawHeader()
    {
        GUILayout.BeginArea(headerSectionRect);
        GUILayout.BeginHorizontal();
        bool newData = GUILayout.Button("New Data", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(20) });
        bool loadData = GUILayout.Button("Load Data", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(20) });
        bool saveData = GUILayout.Button("Save Data", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(20) });
        bool addAlignment = GUILayout.Button("Add Alignment", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(20) });
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        if (saveData)
            SaveData();

        if (addAlignment)
            tree.AddAlignment();

        if (loadData)
            LoadData();

        if (newData)
            NewData();
    }
    #endregion

    #region Display Functions. 
    private void InitDisplayTexture()
    {
        displayTexture = new Texture2D(1, 1);
        Color color = new Color(0.15f, 0.15f, 0.15f, 0.5f);
        displayTexture.SetPixel(0, 0, color);
        displayTexture.Apply();
    }

    private void DrawAlignmentDisplay()
    {
        bool remove;
        XML_Alignment alignment;
        List<XML_Alignment> alignmentsToRemove = new List<XML_Alignment>();
        GUILayout.BeginArea(displaySectionRect);
        GUILayout.BeginVertical();
        DrawAlignmentHeader();
        NLin_HelperFunctions.DrawUILine(Color.black);
        foreach (XML_Alignment alignments in tree.alignments)
        {
            alignment = alignments;
            DrawAlignment(ref alignment, out remove);
            NLin_HelperFunctions.DrawUILine(Color.black);
            if (remove)
            {
                alignmentsToRemove.Add(alignment);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        tree.RemoveAlignments(alignmentsToRemove);
    }
    #endregion

    #region Data Handling

    public void NewData()
    {
        tree = new XML_AlignmentsTree();
        tree.alignments.Add(new XML_Alignment() { name = "New Alignment", identifier = 0, initalValue = 0 });
    }

    public void LoadData()
    {
        Debug.Log("Loading Data");
        tree = NLin_XMLSerialization.Deserialize<XML_AlignmentsTree>(XMLFileNames.alignmentTreeFilename);
    }

    public void SaveData()
    {
        Debug.Log("Data saving requested, Saving data.");
        NLin_XMLSerialization.Serialize<XML_AlignmentsTree>(tree, XMLFileNames.alignmentTreeFilename);
    }

    #endregion

    #region Draw Elements. 

    private void DrawAlignmentHeader()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Alignment Name (ID)", GUILayout.Width(150)); 
        GUILayout.Label("Player Starting Value", GUILayout.Width(150));
        GUILayout.Space(15);
        GUILayout.Label("Range:", GUILayout.Width(60));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(310);
        GUILayout.Label("Min", GUILayout.Width(50));
        GUILayout.Label("Max");
        GUILayout.EndHorizontal();
    }

    public void DrawAlignment(ref XML_Alignment alignment, out bool remove)
    {
        GUILayout.BeginHorizontal();
        //Draw Alignment Name and Identifier. 
        alignment.name = GUILayout.TextField(alignment.name, GUILayout.Width(100));
        GUILayout.Label(" (ID: " + alignment.identifier + ")", GUILayout.Width(50));

        //Draw Alignment Value. 
        GUILayout.Space(30);
        alignment.initalValue = EditorGUILayout.FloatField(alignment.initalValue, GUILayout.Width(50));
        GUILayout.Space(60);
        alignment.valueMinimumCap = EditorGUILayout.FloatField(alignment.valueMinimumCap, GUILayout.Width(50));
        alignment.valueMaximumCap = EditorGUILayout.FloatField(alignment.valueMaximumCap, GUILayout.Width(50));
        GUILayout.Space(60);
        remove = GUILayout.Button("Remove alignment", GUILayout.Width(120));
        GUILayout.EndHorizontal();
    }
    #endregion
}