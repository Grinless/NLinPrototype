using System.Xml.Serialization;

/// <summary>
/// XML data class responsible for containing alignment data. 
/// </summary>
public class NLin_XML_Alignment
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
