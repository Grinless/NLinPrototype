using System.Collections.Generic;
using System.Xml.Serialization;

public class NLin_XML_Prop
{
    /// <summary>
    /// The name assigned to the prop. 
    /// </summary>
    [XmlElement("propName")]
    public string name;

    /// <summary>
    /// The identifier assigned to the prop. 
    /// </summary>
    [XmlElement("propIdentifier")]
    public int identifier;

    /// <summary>
    /// The name assigned to the prop. 
    /// </summary>
    [XmlElement("propDesc")]
    public string description;

    /// <summary>
    /// The alignments assigned to the prop. 
    /// </summary>
    [XmlArray("propAlignments")]
    public List<NLin_XML_AlignmentProp> alignments;

    /// <summary>
    /// The dialogue contained within the prop. 
    /// </summary>
    public NLin_XML_PropDialogue dialogue; 

    /// <summary>
    /// Flag representing whether prop identification data should be displayed. 
    /// </summary>
    [System.NonSerialized]
    public bool displayPropIdData = false;

    /// <summary>
    /// Flag representing whether prop alignment data should be displayed. 
    /// </summary>
    [System.NonSerialized]
    public bool displayAlignments = false;

    /// <summary>
    /// Flag representing whether prop should be removed. 
    /// </summary>
    [System.NonSerialized]
    public bool remove = false;

    /// <summary>
    /// Function used to add a new alignment to selected prop.
    /// </summary>
    public void AddAlignment()
    {
        if(alignments == null)
        {
            alignments = new List<NLin_XML_AlignmentProp>();
        }

        alignments.Add(
            new NLin_XML_AlignmentProp()
            {
                name = "New pAlign",
                identifier = 0,
                effectValue = 0
            }
            );
    }
}
