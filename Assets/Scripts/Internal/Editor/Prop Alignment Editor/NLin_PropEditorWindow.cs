using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class responsible for drawing and handling Prop data as an editor. 
/// </summary>
public class NLin_PropEditorWindow : EditorWindow
{
    private NLin_XML_PropDatabase database;
    private Vector2 propScrollPos = Vector2.zero;
    private Vector2 alignmentScrollPos = Vector2.zero;

    [MenuItem("NLin/Alignments/PropAlignmentEditor")]
    public static void ShowWindow()
    {
        NLin_PropEditorWindow window = GetWindow<NLin_PropEditorWindow>();
        window.titleContent = new UnityEngine.GUIContent("Prop Alignment Editor");
    }

    private void OnEnable()
    {
        database = new NLin_XML_PropDatabase();
        database.props = new List<NLin_XML_PropData>();
        database.AddProp();
    }

    private void OnFocus()
    {
        database = new NLin_XML_PropDatabase();
        database.props = new List<NLin_XML_PropData>();
        database.AddProp();
    }

    private void OnGUI()
    {
        DrawMenu();
        DrawDatabase();
    }

    private void DrawMenu()
    {
        bool createProp = false;

        GUILayout.BeginHorizontal();
        createProp = GUILayout.Button("Add Prop.");
        GUILayout.EndHorizontal();

        if (createProp)
        {
            database.props.Add(new NLin_XML_PropData());
        }
    }

    /// <summary>
    /// Method handling drawing the database and related method calls. 
    /// </summary>
    private void DrawDatabase()
    {
        NLin_XML_PropData _dataSel;

        //Draw the inital divider and title. 
        NLin_HelperFunctions.DrawUILine(Color.black);
        propScrollPos = GUILayout.BeginScrollView(propScrollPos);

        //For each item draw the data in the editor. 
        foreach (NLin_XML_PropData item in database.props)
        {
            _dataSel = item;
            DrawProp(ref _dataSel);
            NLin_HelperFunctions.DrawUILine(Color.black);
        }
        GUILayout.EndScrollView();
    }

    private void DrawProp(ref NLin_XML_PropData propData)
    {
        NLin_XML_PropData _dataSel = propData;

        DrawPropHeader(ref _dataSel);

        if (_dataSel.displayPropIdData)
            DrawIdentificationEditing(ref _dataSel);

        if (_dataSel.displayAlignments)
        {
            DrawAlignmentsHeader(ref _dataSel);
            DrawDescription(ref _dataSel);
            DrawAlignments(ref _dataSel);
        }
    }

    /// <summary>
    /// Draw the header and header options for the prop. 
    /// </summary>
    /// <param name="data"> The data instance to draw. </param>
    private void DrawPropHeader(ref NLin_XML_PropData data)
    {
        //Draw identification data. 
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prop(" + data.name + ") ID: " + data.identifier + ". ");
        //Provide display identification toggle. 
        GUILayout.Label("Display:");
        data.displayPropIdData = GUILayout.Toggle(data.displayPropIdData, " Identification");
        data.displayAlignments = GUILayout.Toggle(data.displayAlignments, " Alignments");
        data.remove = GUILayout.Button("Remove Prop");
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Function handling drawing data modification fields for prop identification.
    /// </summary>
    /// <param name="data"> The prop data instance to display/modify. </param>
    private void DrawIdentificationEditing(ref NLin_XML_PropData data)
    {
        if (data.displayPropIdData)
        {
            //Display identification data fields. 
            GUILayout.BeginHorizontal();
            GUILayout.Label("id: ", GUILayout.Width(20));
            GUILayout.Space(10);
            data.identifier = EditorGUILayout.IntField(data.identifier, GUILayout.Width(40));
            GUILayout.Space(20);
            GUILayout.Label("name: ", GUILayout.Width(40));
            data.name = EditorGUILayout.TextField(data.name, GUILayout.Width(150));
            GUILayout.EndHorizontal();
        }
    }

    private void DrawAlignmentsHeader(ref NLin_XML_PropData data)
    {
        bool addAlignment = false;

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Alignments: ");
        addAlignment = GUILayout.Button("Add");

        if (addAlignment)
            data.AddAlignment();

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    private void DrawDescription(ref NLin_XML_PropData data)
    {
        data.description = EditorGUILayout.TextArea(data.description);
    }

    private void DrawAlignments(ref NLin_XML_PropData data)
    {
        string[] options = NLin_EditorHelper.CurrentAlignments;

        if (data.alignments != null)
        {
            //Draw the alignments.

            EditorGUILayout.BeginVertical();
            foreach (var alignment in data.alignments)
            {
                EditorGUILayout.BeginHorizontal();
                //Draw alignment name and identifier.
                EditorGUILayout.LabelField("ID: " + alignment.identifier, GUILayout.Width(20));
                alignment.identifier = EditorGUILayout.Popup("", alignment.identifier, options);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }
}
