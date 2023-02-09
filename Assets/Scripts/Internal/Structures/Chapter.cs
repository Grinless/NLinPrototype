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

    public string Title
    {
        get { return _name; }
    }

    public string Type
    {
        get { return _type.ToString(); }
    }

    public Chapter(int id, string title, ChapterType type)
    {
        _id = id;
        _name = title; 
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

    public bool CompareChapter(Chapter chapter, int id, string title, ChapterType type)
    {
        if (chapter.ID == id && chapter.Title == title && chapter.Type.ToString() == type.ToString())
            return true;
        else
            return false;
    }
}
