using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class XML_Alignments
{
    [XmlElement(ElementName = "name")]
    public string name;
    [XmlElement(ElementName = "identifier")]
    public int identifier;
    [XmlElement(ElementName = "initalValue")]
    public float initalValue;
    [XmlElement(ElementName = "minimumCap")]
    public float valueMinimumCap;
    [XmlElement(ElementName = "maximumCap")]
    public float valueMaximumCap;
}

[XmlRoot(ElementName = "AlignmentTree")]
public class XML_AlignmentsTree
{
    [XmlArray(ElementName = "Alignments")]
    public List<XML_Alignments> alignments = new List<XML_Alignments>();

    #region Data Editing. 
    public void AddAlignment() =>
    alignments.Add(
        new XML_Alignments()
        {
            name = "New Alignment",
            identifier = GetNextIdentifier(),
            initalValue = 0
        });

    private int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_Alignments item in alignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }
    #endregion
}

public enum RoomTypeEnum
{
    START_NODE,
    PASSAGE_NODE,
    ENDING_NODE
}

[XmlRoot(ElementName = "RoomData")]
public class XML_RoomTree
{
    [XmlArray(ElementName = "Rooms")]
    public List<XML_RoomData> rooms = new List<XML_RoomData>();

    #region Data Editing. 
    public void AddRoom()
    {
        rooms.Add(
                new XML_RoomData()
                {
                    name = "New room",
                    identifier = GetNextIdentifier(),
                    roomType = RoomTypeEnum.ENDING_NODE
                });
    }

    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_RoomData item in rooms)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }
    #endregion
}

[System.Serializable]
public class XML_RoomData
{
    [XmlElement(ElementName = "name")]
    public string name;
    [XmlElement(ElementName = "identifier")]
    public int identifier;
    [XmlArray(ElementName = "roomAlignments")]
    public List<XML_RoomAlignment> roomAlignments = new List<XML_RoomAlignment>();
    [XmlElement(ElementName = "type")]
    public RoomTypeEnum roomType = RoomTypeEnum.START_NODE;

    #region Data Editing. 
    public void AddAlignment()
    {
        roomAlignments.Add(
            new XML_RoomAlignment()
            {
                identifier = GetNextIdentifier()
            }
            );
    }

    public int GetNextIdentifier()
    {
        int lastID = -1; //This value should never be set, hence making it safe for comparison. 

        //Iterate over existing entry identifiers and increment by 1. 
        foreach (XML_RoomAlignment item in roomAlignments)
            if (lastID < item.identifier)
                lastID = item.identifier;
        return lastID + 1;
    }

    public void ResolveAndSetRoomType(int selection)
    {
        switch (selection)
        {
            case 0:
                roomType = RoomTypeEnum.START_NODE;
                break;
            case 1:
                roomType = RoomTypeEnum.PASSAGE_NODE;
                break;
            case 2:
                roomType = RoomTypeEnum.ENDING_NODE;
                break;
            default:
                roomType = RoomTypeEnum.START_NODE;
                break;
        }
    }

    public void RemoveAlignments(List<XML_RoomAlignment> alignments)
    {
        lock (alignments)
        {
            foreach (XML_RoomAlignment item in alignments)
            {
                roomAlignments.Remove(item);
            }
        }
    }
    #endregion
}

[System.Serializable]
public class XML_RoomAlignment
{
    [XmlElement(ElementName = "identifier")]
    public int identifier;
    [XmlElement(ElementName = "matchMin")]
    public float matchMin;
    [XmlElement(ElementName = "matchMax")]
    public float matchMax;
    [XmlElement(ElementName = "thresholdMin")]
    public float thresholdMin;
    [XmlElement(ElementName = "thresholdMax")]
    public float thresholdMax;
}

public static class XMLFileNames
{
    public static string atfilename = "/align.xml";
    public static string rtfilename = "/alignrt.xml";
}

public static class XMLSerialization
{
    private static string folder = "/Alignments";

    private static string DirPath
    {
        get { return Application.dataPath + folder; }
    }

    public static void Serialize<T>(T type, string fileName)
    {
        Debug.Log("Serialization called");
        DirectoryCheck(DirPath);
        XmlSerializer s = new XmlSerializer(typeof(T));
        Stream stream = new FileStream(DirPath + fileName, FileMode.Create);
        s.Serialize(stream, type);
        stream.Close();
    }

    public static T Deserialize<T>(string fileName)
    {
        Debug.Log("Deserialization called");
        DirectoryCheck(DirPath);
        XmlSerializer s = new XmlSerializer(typeof(T));
        Stream stream = new FileStream(DirPath + fileName, FileMode.OpenOrCreate);
        var output = s.Deserialize(stream);
        stream.Close();
        return (T)output;
    }

    private static void DirectoryCheck(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }

    private static bool FileCheck(string filePath)
    {
        if (File.Exists(filePath))
        {
            return true;
        }

        return false;
    }
}
