using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChapterType
{
    START, 
    LINKED_NODE,
    END
}

public class Chapter
{
    private int _id; 
    private string _name;
    private ChapterType _type;

    public int ID
    {
        get { return _id; }
    }

    public string Name
    {
        get { return _name; }
    }

    public string Type
    {
        get { return _type.ToString(); }
    }

    public Chapter(int ID, string name, ChapterType type)
    {
        _id = ID;
        _name = name; 
        _type = type;
    }

    public bool CheckChapterType(ChapterType type)
    {
        if(type == _type)
        {
            return true;
        }
        return false;
    }
}