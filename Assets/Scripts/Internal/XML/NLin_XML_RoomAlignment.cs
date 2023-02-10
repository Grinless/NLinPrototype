using System.Xml.Serialization;

/// <summary>
/// Serializable data class for a room alignment. 
/// </summary>
[System.Serializable]
public class NLin_XML_RoomAlignment
{
    /// <summary>
    /// The identifier assigned to the room alignment. 
    /// </summary>
    [XmlElement(ElementName = "identifier")]
    public int identifier;

    /// <summary>
    /// The minium match condition range. 
    /// </summary>
    [XmlElement(ElementName = "matchRange")]
    public NLin_XML_Range matchRange;

    /// <summary>
    /// The minium match condition range. 
    /// </summary>
    [XmlElement(ElementName = "thresholdRange")]
    public NLin_XML_Range thresholdRange; 
}