using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoryTests
{
    Story _testStory;
    List<Chapter> _chapterList = new List<Chapter>()
    {
        new Chapter(0, "Awaken", ChapterType.START),
        new Chapter(0, "Submerged", ChapterType.START),
        new Chapter(0, "Godhood", ChapterType.START),
        new Chapter(1, "Trial By Fire", ChapterType.LINKED_NODE),
        new Chapter(1, "Death Of The Soul", ChapterType.END)
    };


    [SetUp]
    public void TestSetup()
    {
        _testStory = new Story(_chapterList);
    }

    [Test]
    public void Story_ClassConstruction()
    {
        Assert.IsNotNull(_testStory);
    }

    [Test]
    public void Story_ChapterAddition()
    {
        Assert.AreEqual(5, _testStory.ChapterCount);
        _testStory.AddChapter(new Chapter(1, "Death Of The Soul", ChapterType.END));
        Assert.AreEqual(6, _testStory.ChapterCount);
    }

    [Test]
    public void Story_StartChapterLoad()
    {
        Assert.IsNotNull(_testStory.CurrentChapter);
    }

    [Test]
    public void Story_ChapterLoadNext()
    {
        int id;
        id = _testStory.CurrentChapter.ID; 
        _testStory.GoToNextChapter();
        Assert.AreNotEqual(id, _testStory.CurrentChapter.ID);
    }

    //[Test]
    //public void Story_ChapterLoadByIndex()
    //{
    //    Assert.Fail();
    //}

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator StoryTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
