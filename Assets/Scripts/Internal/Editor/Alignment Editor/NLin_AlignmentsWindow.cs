using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

/// <summary>
/// Editor used to assist alignment creation in N_Lin. 
/// </summary>
public class NLin_AlignmentsWindow : EditorWindow
{
    [SerializeField]
    private Rect headerSectionRect = new Rect() { x = 0, y = 0, width = Screen.width, height = 20 };
    private Rect displaySectionRect = new Rect() { x = 0, y = 20.1f, height = 500 };
    private Texture2D headerTexture;
    private Texture2D displayTexture;

    private NLin_XML_AlignmentDefTree Tree => NLin_EditorManager.AlignmentDefTree;

    [MenuItem("NLin/Alignments/AlignmentEditor")]
    public static void ShowWindow()
    {
        NLin_AlignmentsWindow wnd = GetWindow<NLin_AlignmentsWindow>();
        wnd.titleContent = new GUIContent("AlignmentsEditor");
    }

    private void OnEnable()
    {
        InitTextures();
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawAlignmentDisplay();
    }

    private void InitTextures()
    {
        InitHeaderTexture();
        InitDisplayTexture();
    }

    private void DrawLayouts()
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
            NLin_EditorManager.AddAlignmentDef(); 

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
        NLin_XML_AlignmentDef alignment;
        List<NLin_XML_AlignmentDef> alignmentsToRemove = new List<NLin_XML_AlignmentDef>();
        GUILayout.BeginArea(displaySectionRect);
        GUILayout.BeginVertical();
        DrawAlignmentHeader();
        NLin_HelperFunctions.DrawUILine(Color.black);
        foreach (NLin_XML_AlignmentDef alignments in Tree.alignments)
        {
            alignment = alignments;
            NLin_EGUIDrawer_Alignment.Draw(ref alignment, out remove);
            NLin_HelperFunctions.DrawUILine(Color.black);
            if (remove)
            {
                alignmentsToRemove.Add(alignment);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        Tree.RemoveAlignments(alignmentsToRemove);
    }
    #endregion

    private void NewData() => 
        NLin_EditorManager.NewAlignmentDefTree();

    private void LoadData() => 
        NLin_XML_Serialization.Deserialize<NLin_XML_AlignmentDefTree>(XMLFileNames.alignmentTreeFilename);

    private void SaveData() => 
        NLin_EditorManager.SaveAlignmentDefTree();

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
    #endregion
}