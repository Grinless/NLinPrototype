using System.Collections.Generic;
using System.Xml.Serialization;
/// <summary>
/// Serializable class for room data. 
/// </summary>
[System.Serializable]
public class NLin_XML_Room
{
    /// <summary>
    /// The name of the room. 
    /// </summary>
    [XmlElement(ElementName = "name")]
    public string name;

    /// <summary>
    /// The rooms identifier. 
    /// </summary>
    [XmlElement(ElementName = "identifier")]
    public int identifier;

    /// <summary>
    /// The list of alignments associated with the room. 
    /// </summary>
    [XmlArray(ElementName = "roomAlignments")]
    public List<NLin_XML_RoomAlignment> roomAlignments = new List<NLin_XML_RoomAlignment>();

    /// <summary>
    /// The room type. 
    /// </summary>
    [XmlElement(ElementName = "type")]
    public NLin_XML_RoomTypeEnum roomType = NLin_XML_RoomTypeEnum.START_NODE;

    public bool AlignmentsMaxed => roomAlignments.Count > NLin_EditorHelper.AlignmentCap;

    #region Data Editing. 

    /// <summary>
    /// Add a new alignment to the room. 
    /// </summary>
    public void AddAlignment()
    {
        int nextID = GetNextIdentifier();

        if (AlignmentsMaxed)
            return;

        roomAlignments.Add(
            new NLin_XML_RoomAlignment() { 
                identifier = nextID, 
                matchRange = new NLin_XML_Range() { max = 1, min = -1},
                thresholdRange = new NLin_XML_Range() { max = 1, min = -1}
            }
            );
    }

    /// <summary>
    /// Get the next identifier associated with the room. 
    /// </summary>
    /// <returns> Returns the next identifier in the alignment tree. </returns>
    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (NLin_XML_RoomAlignment item in roomAlignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }

    public int GetMissingIdentifier()
    {
        for (int i = 0; i < roomAlignments.Count; i++)
        {
            if ((roomAlignments[i].identifier + 1) != roomAlignments[i].identifier)
            {
                return i + 1;
            }
        }

        return -1;
    }

    public bool CheckMissingIdentifier() =>
        (roomAlignments.Count < NLin_EditorHelper.AlignmentCap) ? true : false;



    /// <summary>
    /// Set a room type using a RoomType enum identifier integer.
    /// </summary>
    /// <param name="selection"> The currently selected integer identifier. </param>
    public void ResolveAndSetRoomType(int selection)
    {
        switch (selection)
        {
            case 0:
                roomType = NLin_XML_RoomTypeEnum.START_NODE;
                break;
            case 1:
                roomType = NLin_XML_RoomTypeEnum.PASSAGE_NODE;
                break;
            case 2:
                roomType = NLin_XML_RoomTypeEnum.ENDING_NODE;
                break;
            default:
                roomType = NLin_XML_RoomTypeEnum.START_NODE;
                break;
        }
    }

    /// <summary>
    /// Remove a set of alignments from the room. 
    /// </summary>
    /// <param name="alignments"> The alignments to remove from the room. </param>
    public void RemoveAlignments(List<NLin_XML_RoomAlignment> alignments)
    {
        lock (alignments)
        {
            foreach (NLin_XML_RoomAlignment item in alignments)
            {
                roomAlignments.Remove(item);
            }
        }
    }
    #endregion
}
