using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Static class containing references to data files used by the NLin editors. 
/// Also responsible for data parsing calls. 
/// </summary>
public static class NLin_EditorHelper
{
    private static NLin_XML_AlignmentDefTree _alignmentsTree = null;
    private static NLin_XML_RoomTree _roomTree = null;
    private static NLin_XML_AlignmentDef _selectedAlignment = null;
    private static NLin_XML_Room _selectedRoomData = null;
    private static List<string> _alignmentTypes = null;
    private static string[] _roomTypes = null;

    public delegate void AlignmentsManagement();
    public static AlignmentsManagement AlignmentsChanged;

    public static int AlignmentCap => CurrentAlignments.Length - 1;

    public static NLin_XML_AlignmentDefTree AlignmentsTree
    {
        get
        {
            if (_alignmentsTree == null)
            {
                _alignmentsTree = NLin_XMLSerialization.Deserialize<NLin_XML_AlignmentDefTree>(XMLFileNames.alignmentTreeFilename);
            }

            return _alignmentsTree;
        }
    }

    public static NLin_XML_RoomTree RoomTree
    {
        get
        {
            if (_roomTree == null)
            {
                _roomTree = NLin_XMLSerialization.Deserialize<NLin_XML_RoomTree>(XMLFileNames.roomTreeFilename);
            }

            return (_roomTree);
        }
    }

    public static string[] CurrentAlignments
    {
        get
        {
            _alignmentTypes = LoadAlignments();

            return _alignmentTypes.ToArray();
        }
    }

    public static string[] CurrentRoomTypes
    {
        get
        {
            _roomTypes = LoadRoomTypes();
            return _roomTypes;
        }
    }

    public static NLin_XML_AlignmentDef SelectedAlignment
    {
        get { return _selectedAlignment; }
        set { _selectedAlignment = value; }
    }

    public static NLin_XML_Room SelectedRoomData
    {
        get { return _selectedRoomData; }
        set { _selectedRoomData = value; }
    }

    public static NLin_XML_AlignmentDefTree NewAlignmentDefTree()
    {
        _alignmentsTree = new NLin_XML_AlignmentDefTree();
        _alignmentsTree.AddAlignment();
        return _alignmentsTree;
    }

    public static void AddAlignment()
    {
        _alignmentsTree.AddAlignment(); 
    }

    public static void SaveAlignmentTree()
    {
        NLin_XMLSerialization.Serialize<NLin_XML_AlignmentDefTree>(_alignmentsTree, XMLFileNames.alignmentTreeFilename);
        if (AlignmentsChanged != null)
        {
            AlignmentsChanged();
        }
    }

    private static List<string> LoadAlignments()
    {
        List<string> temp = new List<string>();

        for (int i = 0; i < AlignmentsTree.alignments.Count; i++)
        {
            temp.Add(AlignmentsTree.alignments[i].name);
        }

        return temp;
    }

    private static string[] LoadRoomTypes()
    {
        return Enum.GetNames(typeof(NLin_XML_RoomTypeEnum));
    }
}
