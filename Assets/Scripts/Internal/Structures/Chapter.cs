using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum types representing chapter types. 
/// </summary>
public enum ChapterType
{
    START, 
    LINKED_NODE,
    END
}

public class Chapter
{
    /// <summary>
    /// The id of the chapter. 
    /// </summary>
    private int _id;
    
    /// <summary>
    /// The name of the chapter. 
    /// </summary>
    private string _name;

    /// <summary>
    /// The type of the chapter. 
    /// </summary>
    private ChapterType _type;

    /// <summary>
    /// The chapters ID. 
    /// </summary>
    public int ID
    {
        get { return _id; }
    }

    /// <summary>
    /// The Chapters title. 
    /// </summary>
    public string Title
    {
        get { return _name; }
    }

    /// <summary>
    /// The type of the chapter. 
    /// </summary>
    public string Type
    {
        get { return _type.ToString(); }
    }

    /// <summary>
    /// CTOR:
    /// </summary>
    /// <param name="id"> The id of the chapter.</param>
    /// <param name="title"> The title of the chapter. </param>
    /// <param name="type"> The type of chapter. </param>
    public Chapter(int id, string title, ChapterType type)
    {
        _id = id;
        _name = title; 
        _type = type;
    }

    /// <summary>
    /// Check the type of the chapter. 
    /// </summary>
    /// <param name="type"> The type to compare against. </param>
    /// <returns> Boolean flag. </returns>
    public bool CheckChapterType(ChapterType type)
    {
        if(type == _type)
            return true;
        else
           return false;
    }
}
