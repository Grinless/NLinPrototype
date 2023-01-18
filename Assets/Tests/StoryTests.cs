using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoryTests
{
    Story _testStory;
    List<Chapter> _chapterList;

    private void ST_BuildLongList()
    {
        _testStory = new Story(
                    new List<Chapter>() {
                        new Chapter(0, "Chapter 0.0", ChapterType.START),
                        new Chapter(1, "Chapter 0.1", ChapterType.START),
                        new Chapter(2, "Chapter 0.2", ChapterType.START),
                        new Chapter(3, "Chapter 1", ChapterType.LINKED_NODE),
                        new Chapter(4, "Chapter 2", ChapterType.LINKED_NODE),
                        new Chapter(5, "Chapter 3", ChapterType.LINKED_NODE),
                        new Chapter(6, "Chapter Epilogue 0.0", ChapterType.END),
                        new Chapter(7, "Chapter Epilogue 0.1", ChapterType.END),
                        new Chapter(8, "Chapter Epilogue 0.2", ChapterType.END)
                    }
                );
    }

    private void ST_BuildOneItemList()
    {
        _testStory = new Story(
            new List<Chapter>() {
                new Chapter(0, "Chapter 0.0", ChapterType.START)
            }
        );
    }

    [Test]
    public void StoryTest_OneItemListInit()
    {
        ST_BuildOneItemList();
        Assert.IsNotNull(_testStory);
    }

    [Test]
    public void StoryTest_MultiItemListInit()
    {
        ST_BuildLongList();
        Assert.IsNotNull(_testStory);
    }

    [Test]
    public void StoryTest_LoadByIndex()
    {
        ST_BuildLongList();

        _testStory.GoToChapterByIdentifier(0);
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 0, "Chapter 0.0", ChapterType.START));

        _testStory.GoToChapterByIdentifier(3);
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 3, "Chapter 1", ChapterType.LINKED_NODE));

        _testStory.GoToChapterByIdentifier(6);
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 6, "Chapter Epilogue 0.0", ChapterType.END));
    }

    [Test]
    public void StoryTest_LoadByTitle()
    {
        ST_BuildLongList();

        _testStory.GoToChapterByIdentifier("Chapter 0.0");
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 0, "Chapter 0.0", ChapterType.START));

        _testStory.GoToChapterByIdentifier("Chapter 1");
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 3, "Chapter 1", ChapterType.LINKED_NODE));
        
        _testStory.GoToChapterByIdentifier("Chapter Epilogue 0.0");
        Assert.IsTrue(CompareChapter(_testStory.CurrentChapter, 6, "Chapter Epilogue 0.0", ChapterType.END));
    }

    private bool CompareChapter(Chapter chapter, int id, string title, ChapterType type)
    {
        if(chapter.ID == id && chapter.Title == title && chapter.Type.ToString() == type.ToString())
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
}
