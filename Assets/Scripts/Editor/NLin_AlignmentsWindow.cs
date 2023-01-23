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
    public static XML_Alignments selected;

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
        XML_Alignments alignment;
        GUILayout.BeginArea(displaySectionRect);
        GUILayout.BeginVertical();
        NLin_HelperFunctions.DrawUILine(Color.black);
        foreach (XML_Alignments alignments in tree.alignments)
        {
            alignment = alignments;
            DrawAlignment(ref alignment);
            NLin_HelperFunctions.DrawUILine(Color.black);
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion

    #region Data Handling

    public void NewData()
    {
        tree = new XML_AlignmentsTree();
        tree.alignments.Add(new XML_Alignments() { name = "New Alignment", identifier = 0, initalValue = 0 });
    }

    public void LoadData()
    {
        Debug.Log("Loading Data");
        tree = XMLSerialization.Deserialize<XML_AlignmentsTree>(XMLFileNames.atfilename);
    }

    public void SaveData()
    {
        Debug.Log("Data saving requested, Saving data.");
        XMLSerialization.Serialize<XML_AlignmentsTree>(tree, XMLFileNames.atfilename);
    }

    #endregion

    #region Draw Elements. 

    public void DrawAlignment(ref XML_Alignments alignment)
    {
        GUILayout.BeginHorizontal();
        //Draw Alignment Name and Identifier. 
        GUILayout.Label(alignment.name + " (ID: " + alignment.identifier + "): ", GUILayout.Width(150));

        //Draw Alignment Value. 
        GUILayout.Label("Starting Value (Player):", GUILayout.Width(150));
        alignment.initalValue = EditorGUILayout.FloatField(alignment.initalValue, GUILayout.Width(50));
        GUILayout.Space(50);
        bool edit = GUILayout.Button("Edit alignment", GUILayout.Width(100));
        GUILayout.EndHorizontal();

        if (edit)
        {
            //Launch new editor. 
            Debug.Log("Edit data");
            selected = alignment;
            NLin_AlignmentEditor.CreateWizard();

        }
    }
    #endregion
}

/// <summary>
/// ScriptableWizard used to further edit a specified alignment value. 
/// </summary>
public class NLin_AlignmentEditor : ScriptableWizard
{
    public string a_title = NLin_AlignmentsWindow.selected.name;
    public float a_initalValue = NLin_AlignmentsWindow.selected.initalValue;
    public float a_minRange = NLin_AlignmentsWindow.selected.valueMinimumCap;
    public float a_maxRange = NLin_AlignmentsWindow.selected.valueMaximumCap;

    public static void CreateWizard()
    {
        //Generate the new wizard. 
        ScriptableWizard.DisplayWizard<NLin_AlignmentEditor>("Alignment Editor", "Update", "Cancel");
    }

    private void OnWizardCreate()
    {
        //On create set all values to the inital alignment class. 
        NLin_AlignmentsWindow.selected.name = a_title;
        NLin_AlignmentsWindow.selected.initalValue = a_initalValue;
        NLin_AlignmentsWindow.selected.valueMinimumCap = a_minRange;
        NLin_AlignmentsWindow.selected.valueMaximumCap = a_maxRange;
    }

    private void OnWizardOtherButton() => this.Close(); //If Closed button pressed, changes should be ignored.
}