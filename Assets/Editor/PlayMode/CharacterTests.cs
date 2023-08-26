using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MGCNTN.Core
{
    public class CharacterTests
    {

        private bool isReady = false;
        private Player sut; //System Under Test
        private Map map;
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
            map = GameObject.FindObjectOfType<Map>();
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

        [UnityTest]
        public IEnumerator Character_facing_updates_correctly()
        {
            while (!isReady) { yield return null; }

            sut.turner.Turn(Direction.Down);
            Assert.AreEqual(Direction.Down, sut.facing);

            sut.turner.Turn(Direction.Left);
            Assert.AreEqual(Direction.Left, sut.facing);

            sut.turner.Turn(Direction.Right);
            Assert.AreEqual(Direction.Right, sut.facing);

            sut.turner.Turn(Direction.Up);
            Assert.AreEqual(Direction.Up, sut.facing);

        }

        [UnityTest]
        public IEnumerator Updates_cell_map_dictionary()
        {
            while (!isReady) { yield return null; }

            Vector2Int firstCell = sut.currCell;

            Assert.IsTrue(map.containsKey(firstCell));
            Assert.AreEqual(sut, map.getOccupuiedCell(firstCell));

            sut.mover.TryMove(Direction.Left);
            yield return new WaitForSeconds(.5f);

            Assert.IsTrue(map.containsKey(sut.currCell));
            Assert.IsFalse(map.containsKey(firstCell));
            Assert.AreEqual(sut, map.getOccupuiedCell(sut.currCell));

        }
    }
}