using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NLin_EditorHelper
{
    private static XML_AlignmentsTree _alignmentsTree;
    private static XML_RoomTree _roomTree;
    private static XML_Alignment _selectedAlignment; 
    private static XML_Room _selectedRoomData;

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

}
