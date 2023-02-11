using System.Xml.Serialization;

/// <summary>
/// XML data class used to define an NLin alignment definition. 
/// </summary>
public class NLin_XML_AlignmentDef
{
    /// <summary>
    /// The name of the alignment. 
    /// </summary>
    [XmlElement(ElementName = "name")]
    public string name;

    /// <summary>
    /// The identifier assigned to the alignment. 
    /// </summary>
    [XmlElement(ElementName = "identifier")]
    public int identifier;

    /// <summary>
    /// The inital starting value of the alignment. 
    /// </summary>
    [XmlElement(ElementName = "initalValue")]
    public float value;

    /// <summary>
    /// The minimum/maximum value the alignment can reach. 
    /// </summary>
    [XmlElement(ElementName = "range")]
    public NLin_XML_Range range;
}
