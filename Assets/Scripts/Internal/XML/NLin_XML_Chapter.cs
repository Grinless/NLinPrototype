using System;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Class responsible for handling the assignment and creation of NLin_Chapters.
/// Stores references to loadable rooms & identification related to a chapter. 
/// </summary>
public class NLin_XML_Chapter
{
    /// <summary>
    /// The identifier assigned to the chapter.
    /// </summary>
    [XmlElement("identifier")]
    public int id;
    
    /// <summary>
    /// The name assigned to the chapter. 
    /// </summary>
    [XmlElement("name")]
    public string name;
    
    /// <summary>
    /// The type assigned to the chapter. 
    /// </summary>
    [XmlElement("chapterType")]
    public ChapterType chapterType;
    
    /// <summary>
    /// The rooms assigned within the chapter. 
    /// </summary>
    [XmlElement("rooms")]
    public NLin_XML_RoomTree Rooms; 
    
    /// <summary>
    /// The list of alignments required to load the given chapter. 
    /// </summary>
    [XmlElement("alignmentList")]
    public List<NLin_XML_Alignment> AlignmentList;
}
