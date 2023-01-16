using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for managing the highest level of non-linear narrative. 
/// Tracks the current non-linear path followed by the player.
/// TODO: Add further randomisation to how chapters are loaded (math should be defined in an external math class). 
/// TODO: 
/// </summary>

public class Story
{
    /// <summary>
    /// The list of available chapters within the story. 
    /// </summary>
    private List<Chapter> _chapters = new List<Chapter>();

    /// <summary>
    /// The chapters that have been accessed/loaded by the player. 
    /// </summary>
    private List<Chapter> _playedChapters = new List<Chapter>();

    /// <summary>
    /// The current active chapter within the narrative. 
    /// </summary>
    private Chapter _currentChapter = null;

    /// <summary>
    /// Get the total count of chapters within the narrative. 
    /// </summary>
    public int ChapterCount
    {
        get { return _chapters.Count; }
    }

    /// <summary>
    /// Get the current chapter the player is within. 
    /// </summary>
    public Chapter CurrentChapter
    {
        get { return _currentChapter; }
    }

    /// <summary>
    /// Add a chapter to the current chapters held within the narrative. 
    /// </summary>
    /// <param name="chapter"> The chapter to add. </param>
    public void AddChapter(Chapter chapter)
    {
        _chapters.Add(chapter);
    }

    /// <summary>
    /// Add a selection of chapters to the story. 
    /// </summary>
    /// <param name="chapters"> The list of chapters to add to the story. </param>
    public void AddChapters(List<Chapter> chapters)
    {
        _chapters.AddRange(chapters);
    }

    /// <summary>
    /// CTOR: 
    /// INIT the story class, and set inital conditionals. 
    /// </summary>
    /// <param name="chapters">The chapters that should be added to the story instance at runtime. </param>
    public Story(List<Chapter> chapters)
    {
        //Set chapters. 
        _chapters = chapters;
        SelectInitalChapter();
    }

    /// <summary>
    /// Select the inital chapter for the story. 
    /// </summary>
    private void SelectInitalChapter()
    {
        Chapter startSelection;

        //Generate starting room. 
        startSelection = RandomiseStartChapter();
#if DEBUG
        Debug.Log(string.Format("Level: <name: {0}, ID: {1}, type: {2}>", _currentChapter.Name, _currentChapter.ID, _currentChapter.Type));
#endif
        GoToChapterByIndex(startSelection.ID);
    }

    /// <summary>
    /// Temp math process for inital room selection. 
    /// </summary>
    /// <returns> A semi-randomised inital room selection. </returns>
    private Chapter RandomiseStartChapter()
    {
        List<Chapter> startChapters = GetChapterByType(ChapterType.START);
        int randomRange = UnityEngine.Random.Range(0, startChapters.Count);
        return startChapters[randomRange];
    }

    /// <summary>
    /// Shorthand function for loading the sequential chapter. 
    /// TODO: Replace with randomisation/allignment values.
    /// </summary>
    public void GoToNextChapter() => GoToChapterByIndex(_currentChapter.ID + 1);

    /// <summary>
    /// Allows for a level to be loaded based on index. 
    /// TODO: Replace with randomisation/allignment values. 
    /// </summary>
    /// <param name="index"> The index of the level to be loaded. </param>
    public void GoToChapterByIndex(int index)
    {
        Chapter chapter = GetChapter(index);

        if (chapter != null)
        {
            _currentChapter = chapter;
            _playedChapters.Add(chapter);
        }
        else
        {
            Debug.LogError(string.Format("Chapter does not exist with index {0}. Chapter load failed.", index));
        }
    }

    /// <summary>
    /// Retrive a specific chapter based on its ID. 
    /// </summary>
    /// <param name="chapterID"> The ID of the chapter to be retrived. </param>
    /// <returns> The requested chapter/NULL if not found. </returns>
    private Chapter GetChapter(int chapterID)
    {
        foreach (Chapter chapter in _chapters)
        {
            if (chapter.ID == chapterID)
            {
                return chapter;
            }
        }

        return null;
    }

    private List<Chapter> GetChapterByType(ChapterType type)
    {
        List<Chapter> matched = new List<Chapter>();

        foreach (Chapter c in _chapters)
        {
            if (c.CheckChapterType(type))
            {
                matched.Add(c);
            }
        }

        return matched;
    }
}
