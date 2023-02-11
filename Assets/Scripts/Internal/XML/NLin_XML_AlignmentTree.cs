using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Data class containing information on core alignment types. 
/// </summary>
[XmlRoot(ElementName = "AlignmentTree")]
public class NLin_XML_AlignmentTree
{
    /// <summary>
    /// The list of serializable alignment types. 
    /// </summary>
    [XmlArray(ElementName = "Alignments")]
    public List<NLin_XML_AlignmentDef> alignments = new List<NLin_XML_AlignmentDef>();

    #region Data Editing. 

    /// <summary>
    /// Add an alignment to the tree. 
    /// </summary>
    public void AddAlignment()
    {
        int nxtIndex = GetNextIdentifier();

        alignments.Add(new NLin_XML_AlignmentDef()
        {
            name = "New Alignment",
            identifier = GetNextIdentifier(),
            value = 0, 
            range = new NLin_XML_Range() { min = -1, max = 1}
        });
    }


    /// <summary>
    /// Get the next Identifier. 
    /// </summary>
    /// <returns> Returns the next identifier in the alignment tree.</returns>
    private int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (NLin_XML_AlignmentDef item in alignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }

    /// <summary>
    /// Remove a list of alignments from the tree. 
    /// </summary>
    /// <param name="alignments"> The list of alignments to remove.</param>
    public void RemoveAlignments(List<NLin_XML_AlignmentDef> alignments)
    {
        lock (this.alignments)
        {
            foreach (NLin_XML_AlignmentDef item in alignments)
            {
                this.alignments.Remove(item);
            }
        }
    }

    #endregion
}
