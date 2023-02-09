using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class NLin_PropBase: MonoBehaviour
{

}

[XmlRoot("PropDatabase")]
public class NLin_XML_PropDatabase
{
    /// <summary>
    /// The array of props contained within the project. 
    /// </summary>
    [XmlArray("Props")]
    public List<NLin_XML_PropData> props; 

    public void AddProp()
    {
        if(props == null)
        {
            props = new List<NLin_XML_PropData>();
        }

        props.Add(new NLin_XML_PropData());
    }
}

public class NLin_XML_PropData
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
    public List<NLin_XML_PropAlignmentData> alignments;

    [System.NonSerialized]
    public bool displayPropIdData = false;

    [System.NonSerialized]
    public bool displayAlignments = false;

    [System.NonSerialized]
    public bool remove = false;

    public void AddAlignment()
    {
        if(alignments == null)
        {
            alignments = new List<NLin_XML_PropAlignmentData>();
        }

        alignments.Add(
            new NLin_XML_PropAlignmentData()
            {
                name = "New pAlign",
                identifier = 0,
                effectValue = 0
            }
            );
    }
}

public class NLin_XML_PropDialogueData
{
    /// <summary>
    /// The set of strings representing the props interaction dialogue. 
    /// </summary>
    [XmlArray("propAlignments")]
    public string[] dialogueStrings; 
}

public class NLin_XML_PropAlignmentData
{
    /// <summary>
    /// The name of the alignment. 
    /// </summary>
    public string name;

    /// <summary>
    /// The identifier associated with the alignment. 
    /// </summary>
    public int identifier;

    /// <summary>
    /// The effect the prop should have on the alignment. 
    /// </summary>
    public float effectValue; 
}