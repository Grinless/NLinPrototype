using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Static class containing references to data files used by the NLin editors. 
/// Also responsible for data parsing calls & Editor event handling. 
/// </summary>
public static class NLin_EditorManager
{
    /// <summary>
    /// The loaded instance of the NLin_XML_AlignmentDefTree. 
    /// </summary>
    private static NLin_XML_AlignmentDefTree _alignmentDefTree = null;

    /// <summary>
    /// The loaded instance of the NLin_XML_BiomeTree. 
    /// </summary>
    private static NLin_XML_BiomeTree _biomeTree = null;

    /// <summary>
    /// The current selected NLin_XML_AlignmentDef. 
    /// </summary>
    private static NLin_XML_AlignmentDef _selectedAlignmentDef = null;

    /// <summary>
    /// The current selected NLin_XML_Biome
    /// </summary>
    private static NLin_XML_Biome _selectedBiome = null;

    /// <summary>
    /// The alignment types as a string array. 
    /// </summary>
    private static List<string> _alignmentTypes = null;

    /// <summary>
    /// The current NLin_NodeType enum state as a string[]. 
    /// </summary>
    private static string[] _nodeTypes = null;

    /// <summary>
    /// Delegate allowing the manager to be notified that changes have occured to data. 
    /// </summary>
    public delegate void AlignmentsManagement();

    /// <summary>
    /// Event providing callbacks when NLin_XML_Alignments data is changed. 
    /// </summary>
    public static AlignmentsManagement AlignmentsChanged;

    /// <summary>
    /// The current maximum number of assigned NLin_XML_AlignmentDefinitions. 
    /// </summary>
    public static int AlignmentDefCap => AlignmentDefTreeSTRArray.Length - 1;

    /// <summary>
    /// The current NLin_XML_AlignmentDefTree data (Readonly). 
    /// </summary>
    public static NLin_XML_AlignmentDefTree AlignmentDefTree
    {
        get
        {
            if (_alignmentDefTree == null)
            {
                _alignmentDefTree = NLin_XML_Serialization.Deserialize<NLin_XML_AlignmentDefTree>(XMLFileNames.alignmentTreeFilename);
            }

            return _alignmentDefTree;
        }
    }

    /// <summary>
    /// The current NLin_XML_BiomeTree data (Readonly). 
    /// </summary>
    public static NLin_XML_BiomeTree BiomeTree
    {
        get
        {
            if (_biomeTree == null)
            {
                _biomeTree = NLin_XML_Serialization.Deserialize<NLin_XML_BiomeTree>(XMLFileNames.roomTreeFilename);
            }

            return (_biomeTree);
        }
    }

    /// <summary>
    /// The current NLin_XML_AlignmentDefTree definitions as a string array (Readonly). 
    /// </summary>
    public static string[] AlignmentDefTreeSTRArray
    {
        get
        {
            _alignmentTypes = LoadAlignmentsDefTree();

            return _alignmentTypes.ToArray();
        }
    }

    /// <summary>
    /// The current NLin_XML_NodeType definitions as a string array (Readonly).
    /// </summary>
    public static string[] CurrentNodeTypesSTRArray
    {
        get
        {
            _nodeTypes = LoadNodeTypes();
            return _nodeTypes;
        }
    }

    /// <summary>
    /// The currently selected NLin_XML_AlignmentDef.  
    /// </summary>
    public static NLin_XML_AlignmentDef SelectedAlignmentDef
    {
        get { return _selectedAlignmentDef; }
        set { _selectedAlignmentDef = value; }
    }

    /// <summary>
    /// The currently selected NLin_XML_Biome.  
    /// </summary>
    public static NLin_XML_Biome SelectedBiome
    {
        get { return _selectedBiome; }
        set { _selectedBiome = value; }
    }

    /// <summary>
    /// Request for a new alignment tree to be created. 
    /// </summary>
    public static void NewAlignmentDefTree()
    {
        _alignmentDefTree = new NLin_XML_AlignmentDefTree();
        _alignmentDefTree.AddAlignment();
    }

    /// <summary>
    /// Add a new NLin_XML_AlignmentDef to the current NLin_XML_AlignmentDefTree
    /// </summary>
    public static void AddAlignmentDef()
    {
        _alignmentDefTree.AddAlignment(); 
    }

    /// <summary>
    /// Save the current NLin_XML_AlignmentDefTree to XML. 
    /// </summary>
    public static void SaveAlignmentDefTree()
    {
        NLin_XML_Serialization.Serialize<NLin_XML_AlignmentDefTree>(_alignmentDefTree, XMLFileNames.alignmentTreeFilename);
        if (AlignmentsChanged != null)
        {
            AlignmentsChanged();
        }
    }

    /// <summary>
    /// Load the latest saved data for NLin_XML_AlignmentDef from file. 
    /// </summary>
    /// <returns></returns>
    private static List<string> LoadAlignmentsDefTree()
    {
        List<string> temp = new List<string>();

        for (int i = 0; i < AlignmentDefTree.alignments.Count; i++)
        {
            temp.Add(AlignmentDefTree.alignments[i].name);
        }

        return temp;
    }

    /// <summary>
    /// Load NLin_XML_NodeTypes as a string[]. 
    /// </summary>
    /// <returns></returns>
    private static string[] LoadNodeTypes() => Enum.GetNames(typeof(NLin_XML_NodeType));
}
