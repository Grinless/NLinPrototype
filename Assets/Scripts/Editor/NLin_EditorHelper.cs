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
    private static XML_AlignmentsTree _alignmentsTree = null;
    private static XML_RoomTree _roomTree = null;
    private static XML_Alignment _selectedAlignment = null; 
    private static XML_Room _selectedRoomData = null;
    private static List<string> _alignmentTypes = null;
    private static string[] _roomTypes = null;

    public delegate void AlignmentsManagement(); 
    public static AlignmentsManagement AlignmentsChanged;

    public static XML_AlignmentsTree AlignmentsTree
    {
        get
        {
            if(_alignmentsTree == null)
            {
                _alignmentsTree = NLin_XMLSerialization.Deserialize<XML_AlignmentsTree>(XMLFileNames.alignmentTreeFilename);
            }

            return _alignmentsTree;
        }
    }

    public static XML_RoomTree RoomTree
    {
        get
        {
            if(_roomTree == null)
            {
                _roomTree = NLin_XMLSerialization.Deserialize<XML_RoomTree>(XMLFileNames.roomTreeFilename);
            }

            return (_roomTree);
        }
    }

    public static string[] CurrentAlignments
    {
        get
        {
            if(_alignmentTypes == null)
            {
                _alignmentTypes = LoadAlignments();
            }

            return _alignmentTypes.ToArray(); 
        }
    }

    public static string[] CurrentRoomTypes
    {
        get
        {
            if (_roomTypes == null)
            {
                _roomTypes = LoadRoomTypes();
            }

            return _roomTypes;
        }
    }

    public static XML_Alignment SelectedAlignment
    {
        get { return _selectedAlignment; }
        set { _selectedAlignment = value; }
    }

    public static XML_Room SelectedRoomData
    {
        get { return _selectedRoomData; }
        set { _selectedRoomData = value; }
    }

    public static XML_AlignmentsTree NewAlignmentTree()
    {
        _alignmentsTree = new XML_AlignmentsTree();
        _alignmentsTree.AddAlignment();
        return _alignmentsTree;
    }

    public static void SaveAlignmentTree()
    {
        NLin_XMLSerialization.Serialize<XML_AlignmentsTree>(_alignmentsTree, XMLFileNames.alignmentTreeFilename);
        if(AlignmentsChanged != null)
        {
            AlignmentsChanged();
        }
    }

    private static List<String> LoadAlignments()
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
        return Enum.GetNames(typeof(RoomTypeEnum));
    }
}
