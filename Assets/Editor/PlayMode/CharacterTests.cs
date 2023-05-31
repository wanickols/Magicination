using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
public class CharacterTests
{

    private bool isReady = false;
    private Player sut; //System Under Test
    Vector2Int curr; //Current Cell


    [OneTimeSetUp]
    public void LoadTestScene()
    {
        SceneManager.LoadScene(0);
        SceneManager.sceneLoaded += sceneReady;
    }

    public void sceneReady(Scene scene, LoadSceneMode mode)
    {
        sut = GameObject.FindObjectOfType<Player>();
        Debug.Log(sut.name);
        isReady = true;
    }

    [UnityTest]
    public IEnumerator CharacterTestsWithEnumeratorPasses()
    {
        while (!isReady) { yield return null; }

        //Left
        move(Direction.Left);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(curr + Direction.Left, sut.currCell);

        //Right
        move(Direction.Right);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(curr + Direction.Right, sut.currCell);

        //Down
        move(Direction.Up);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(curr + Direction.Up, sut.currCell);

        //Up
        move(Direction.Down);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(curr + Direction.Down, sut.currCell);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2);
    }

    private void move(Vector2Int direction)
    {

        //Arrange
        curr = sut.currCell;

        //Act
        sut.mover.TryMove(direction);


    }

}
