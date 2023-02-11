using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("PropDatabase")]
public class NLin_XML_PropDatabase
{
    /// <summary>
    /// The array of props contained within the project. 
    /// </summary>
    [XmlArray("Props")]
    public List<NLin_XML_Prop> props; 

    /// <summary>
    /// Function used to add a new prop to the database. 
    /// </summary>
    public void AddProp()
    {
        if(props == null)
        {
            props = new List<NLin_XML_Prop>();
        }

        props.Add(new NLin_XML_Prop());
    }
}
