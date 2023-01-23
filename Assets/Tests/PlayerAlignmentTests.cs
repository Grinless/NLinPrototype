using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerAlignmentTests
{
    private PlayerAlignment alignment;
    private List<AlignmentType> alignmentTypes;

    //[SetUp]
    //public void TestSetup()
    //{
    //    //Generate test alignment types. 
    //    alignmentTypes = new List<AlignmentType>()
    //    {
    //        new AlignmentType("Love", 0, 0),
    //        new AlignmentType("Anger", 1, 0),
    //        new AlignmentType("Sloth", 2, 0),
    //        new AlignmentType("Pride", 3, 0)
    //    }; 

    //    //Generate the player alignment instance. 
    //    alignment = new PlayerAlignment(alignmentTypes); 
    //}

    //[Test]
    //public void PlayerAlignment_TestConstructor()
    //{
    //    Assert.IsNotNull(alignment);
    //}

    //[Test]
    //public void PlayerAlignment_TestValueAddition()
    //{
    //    alignment.SetAdditiveValue("Love", 0, 1);
    //    Assert.AreEqual( 1, alignment.GetAlignmentType("Love", 0).Value);

    //    alignment.SetAdditiveValue("Love", 0, -0.5f);
    //    Assert.AreEqual(0.5f, alignment.GetAlignmentType("Love", 0).Value);
    //}

    //[Test]
    //public void PlayerAlignment_TestValueSetting()
    //{
    //    AlignmentType type = alignment.GetAlignmentType("Love", 0);
    //    float value = alignment.GetAlignmentType("Love", 0).Value;
    //    alignment.SetNewValue("Love", 0, -0.2f);

    //    Assert.AreEqual(-0.2f, type.Value);
    //    Assert.AreNotEqual(value, type.Value);

    //}

    //[Test]
    //public void PlayerAlignment_AlignmentAddition_TitleAndID()
    //{
    //    AlignmentType typeFound; 
    //    alignment.AddAlignmentType("A", 4);

    //    //Test that set types are contained. 
    //    typeFound = alignment.GetAlignmentType("A", 4);
    //    Assert.AreEqual("A", typeFound.Title); 
    //    Assert.AreEqual(4, typeFound.ID); 
    //    Assert.AreEqual(0, typeFound.Value);
    //}

    //[Test]
    //public void PlayerAlignment_AlignmentAddition_TitleIDValue()
    //{
    //    AlignmentType typeFound;
    //    alignment.AddAlignmentType("A", 4, 0.2f);

    //    //Test that set types are contained. 
    //    typeFound = alignment.GetAlignmentType("A", 4);
    //    Assert.AreEqual("A", typeFound.Title);
    //    Assert.AreEqual(4, typeFound.ID);
    //    Assert.AreEqual(0.2f, typeFound.Value);
    //}

    //[Test]
    //public void PlayerAlignment_AlignmentAddition()
    //{
    //    AlignmentType typeFound;
    //    alignment.AddAlignmentType("A", 4);

    //    //Test that set types are contained. 
    //    typeFound = alignment.GetAlignmentType("A", 4);
    //    Assert.AreEqual("A", typeFound.Title);
    //    Assert.AreEqual(4, typeFound.ID);
    //    Assert.AreEqual(0, typeFound.Value);
    //}

    ////[Test]
    ////public void PlayerAlignment_AlignmentRetrival()
    ////{

    ////}

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator PlayerAlignmentTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
