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