using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Serializable data class for storing a searchable tree of room data classes. 
/// </summary>
[XmlRoot(ElementName = "RoomData")]
public class NLin_XML_RoomTree
{
    /// <summary>
    /// List of rooms contained within a chapter. 
    /// </summary>
    [XmlArray(ElementName = "Rooms")]
    public List<NLin_XML_Room> rooms = new List<NLin_XML_Room>();

    #region Data Editing.

    /// <summary>
    /// Add a room to the room data tree. 
    /// </summary>
    public void AddRoom() =>
        rooms.Add(new NLin_XML_Room()
        {
            name = "New room",
            identifier = GetNextIdentifier(),
            roomType = NLin_XML_RoomTypeEnum.ENDING_NODE
        });

    /// <summary>
    /// Retrive the next identifier for a room from the tree. 
    /// </summary>
    /// <returns> The sequential identifier. </returns>
    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (NLin_XML_Room item in rooms)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }
    #endregion
}
