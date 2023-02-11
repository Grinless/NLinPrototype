using System.Xml.Serialization;

public class NLin_XML_PropDialogue
{
    /// <summary>
    /// The set of strings representing the props interaction dialogue. 
    /// </summary>
    [XmlArray("propAlignments")]
    public string[] dialogueStrings; 
}
