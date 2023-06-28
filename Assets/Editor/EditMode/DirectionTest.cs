using Core;
using NUnit.Framework;
using UnityEngine;

public class DirectionTest
{


    // A Test behaves as an ordinary method
    [Test]
    public void Basic_direction_are_correct()
    {
        //Assert
        Assert.AreEqual(new Vector2Int(0, 1), Direction.Up);
        Assert.AreEqual(new Vector2Int(0, -1), Direction.Down);
        Assert.AreEqual(new Vector2Int(-1, 0), Direction.Left);
        Assert.AreEqual(new Vector2Int(1, 0), Direction.Right);
    }

    [Test]
    public void Is_basic_evalutates_correctly()
    {
        //Arrange
        Vector2Int testVector1 = new Vector2Int(2, 2);
        Vector2Int testVector2 = new Vector2Int(1, 0);
        Vector2Int testVector3 = Direction.Down;
        Vector2Int testVector4 = new Vector2Int(0, 0);

        //Act

        //Assert
        Assert.IsFalse(testVector1.IsBasic());
        Assert.IsTrue(testVector2.IsBasic());
        Assert.IsTrue(testVector3.IsBasic());
        Assert.IsFalse(testVector4.IsBasic());
    }

    //TODO make playmode test for rest of direction
}