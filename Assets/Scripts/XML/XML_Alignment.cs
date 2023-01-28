using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class XML_Alignment
{
    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "name")]
    public string name;
    
    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "identifier")]
    public int identifier;
    
    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "initalValue")]
    public float initalValue;
    
    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "minimumCap")]
    public float valueMinimumCap;
    
    /// <summary>
    /// 
    /// </summary>
    [XmlElement(ElementName = "maximumCap")]
    public float valueMaximumCap;
}

/// <summary>
/// Data class containing information on core alignment types. 
/// </summary>
[XmlRoot(ElementName = "AlignmentTree")]
public class XML_AlignmentsTree
{
    /// <summary>
    /// The list of serializable alignment types. 
    /// </summary>
    [XmlArray(ElementName = "Alignments")]
    public List<XML_Alignment> alignments = new List<XML_Alignment>();

    #region Data Editing. 

    /// <summary>
    /// Add an alignment to the tree. 
    /// </summary>
    public void AddAlignment() =>
    alignments.Add(
        new XML_Alignment()
        {
            name = "New Alignment",
            identifier = GetNextIdentifier(),
            initalValue = 0
        });

    /// <summary>
    /// Get the next Identifier. 
    /// </summary>
    /// <returns> Returns the next identifier in the alignment tree.</returns>
    private int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_Alignment item in alignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }

    /// <summary>
    /// Remove a list of alignments from the tree. 
    /// </summary>
    /// <param name="alignments"> The list of alignments to remove.</param>
    public void RemoveAlignments(List<XML_Alignment> alignments)
    {
        lock (this.alignments)
        {
            foreach (XML_Alignment item in alignments)
            {
                this.alignments.Remove(item);
            }
        }
    }

    #endregion
}

/// <summary>
/// Enum used to specify the type of node a room utilizes. 
/// </summary>
public enum RoomTypeEnum
{
    START_NODE,
    PASSAGE_NODE,
    ENDING_NODE
}

/// <summary>
/// Serializable data class for storing a searchable tree of room data classes. 
/// </summary>
[XmlRoot(ElementName = "RoomData")]
public class XML_RoomTree
{
    /// <summary>
    /// List of rooms contained within a chapter. 
    /// </summary>
    [XmlArray(ElementName = "Rooms")]
    public List<XML_Room> rooms = new List<XML_Room>();

    #region Data Editing.
    
    /// <summary>
    /// Add a room to the room data tree. 
    /// </summary>
    public void AddRoom() =>
        rooms.Add(new XML_Room()
        {
            name = "New room",
            identifier = GetNextIdentifier(),
            roomType = RoomTypeEnum.ENDING_NODE
        });

    /// <summary>
    /// Retrive the next identifier for a room from the tree. 
    /// </summary>
    /// <returns> The sequential identifier. </returns>
    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_Room item in rooms)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }
    #endregion
}

/// <summary>
/// Serializable class for room data. 
/// </summary>
[System.Serializable]
public class XML_Room
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
    public List<XML_RoomAlignment> roomAlignments = new List<XML_RoomAlignment>();

    /// <summary>
    /// The room type. 
    /// </summary>
    [XmlElement(ElementName = "type")]
    public RoomTypeEnum roomType = RoomTypeEnum.START_NODE;

    #region Data Editing. 

    /// <summary>
    /// Add a new alignment to the room. 
    /// </summary>
    public void AddAlignment() =>
        roomAlignments.Add(new XML_RoomAlignment() { identifier = GetNextIdentifier() });

    /// <summary>
    /// Get the next identifier associated with the room. 
    /// </summary>
    /// <returns> Returns the next identifier in the alignment tree. </returns>
    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_RoomAlignment item in roomAlignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }

    /// <summary>
    /// Set a room type using a RoomType enum identifier integer.
    /// </summary>
    /// <param name="selection"> The currently selected integer identifier. </param>
    public void ResolveAndSetRoomType(int selection)
    {
        switch (selection)
        {
            case 0:
                roomType = RoomTypeEnum.START_NODE;
                break;
            case 1:
                roomType = RoomTypeEnum.PASSAGE_NODE;
                break;
            case 2:
                roomType = RoomTypeEnum.ENDING_NODE;
                break;
            default:
                roomType = RoomTypeEnum.START_NODE;
                break;
        }
    }

    /// <summary>
    /// Remove a set of alignments from the room. 
    /// </summary>
    /// <param name="alignments"> The alignments to remove from the room. </param>
    public void RemoveAlignments(List<XML_RoomAlignment> alignments)
    {
        lock (alignments)
        {
            foreach (XML_RoomAlignment item in alignments)
            {
                roomAlignments.Remove(item);
            }
        }
    }
    #endregion
}

/// <summary>
/// Serializable data class for a room alignment. 
/// </summary>
[System.Serializable]
public class XML_RoomAlignment
{
    /// <summary>
    /// The identifier assigned to the room alignment. 
    /// </summary>
    [XmlElement(ElementName = "identifier")]
    public int identifier;

    /// <summary>
    /// The minium match condition. 
    /// </summary>
    [XmlElement(ElementName = "matchMin")]
    public float matchMin;

    /// <summary>
    /// The maximum match condition. 
    /// </summary>
    [XmlElement(ElementName = "matchMax")]
    public float matchMax;

    /// <summary>
    /// The minimum match threshold condition. 
    /// </summary>
    [XmlElement(ElementName = "thresholdMin")]
    public float thresholdMin;

    /// <summary>
    /// The maximum match threshold condition.  
    /// </summary>
    [XmlElement(ElementName = "thresholdMax")]
    public float thresholdMax;
}