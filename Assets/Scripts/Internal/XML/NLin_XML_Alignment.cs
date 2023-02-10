using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class NLin_XML_Alignment
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
    public float initalValue;

    /// <summary>
    /// The minimum/maximum value the alignment can reach. 
    /// </summary>
    [XmlElement(ElementName = "range")]
    public NLin_XML_Range range;
}
