using System.Xml.Serialization;

/// <summary>
/// Serializable class for XML range data. 
/// </summary>
public class NLin_XML_Range
{
    /// <summary>
    /// The minimum value the range allows.  
    /// </summary>
    [XmlElement("minRange")]
    public float min;

    /// <summary>
    /// The maximum value the range allows. 
    /// </summary>
    [XmlElement("maxRange")]
    public float max;
}