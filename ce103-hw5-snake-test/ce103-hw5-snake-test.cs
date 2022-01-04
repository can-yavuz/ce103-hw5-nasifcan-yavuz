using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ce103_hw5_snake_functions;

namespace ce103_hw5_snake_test
{
    [TestClass]
    public class Snake_Test
    {


        [TestMethod]
        public void collisionDetection_test_1()
        {
            ce103snakegame eat = new ce103snakegame();
            int cslwidth = 25;
            int cslheight = 25;
            int[,] snakeloc = new int[5, 200];
            snakeloc[0, 0] = 10;
            snakeloc[1, 0] = 20;
            int snakesize = 10;
            bool situation = eat.collisionDetection(snakeloc, cslwidth, cslheight, snakesize);
            Assert.AreEqual(false, situation);
        }

        [TestMethod]
        public void collisionDetection_test_2()
        {
            ce103snakegame food = new ce103snakegame();
            int cslwidth = 25;
            int cslheight = 25;
            int[,] snakeloc = new int[5, 200];
            snakeloc[0, 0] = 10;
            snakeloc[1, 0] = 20;
            int snakeloc2 = 10;
            bool situation = food.collisionDetection(snakeloc, cslwidth, cslheight, snakeloc2);
            Assert.AreEqual(false, situation);
        }

        [TestMethod]
        public void collisionDetection_test_3()
        {
            ce103snakegame eat = new ce103snakegame();
            int consoleWidth = 25;
            int consoleHeight = 25;
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 10;
            snakeXY[1, 0] = 20;
            int snakeLength = 10;
            bool situation = eat.collisionDetection(snakeXY, consoleWidth, consoleHeight, snakeLength);
            Assert.AreEqual(false, situation);
        }

        [TestMethod]
        public void eatfood_test_1()
        {
            ce103snakegame eat = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 20;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }

        [TestMethod]
        public void eatfood_test_2()
        {
            ce103snakegame eat = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 20;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }

        [TestMethod]
        public void eatfood_test_3()
        {
            ce103snakegame eat = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 20;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, eat.eatFood(snakeXY, foodXY));
        }

        [TestMethod]
        public void collision_snake_test1()
        {
            ce103snakegame collision = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 50;
            snakeXY[1, 0] = 20;
            Assert.AreEqual(false, collision.collisionSnake(29, 3, snakeXY, 9, 1));
        }

        [TestMethod]
        public void collision_snake_test2()
        {
            ce103snakegame collision = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 50;
            snakeXY[1, 0] = 20;
            Assert.AreEqual(false, collision.collisionSnake(29, 3, snakeXY, 9, 1));
        }

        [TestMethod]
        public void collision_snake_test3()
        {
            ce103snakegame collision = new ce103snakegame();
            int[,] snakeXY = new int[5, 200];
            snakeXY[0, 0] = 50;
            snakeXY[1, 0] = 20;
            Assert.AreEqual(false, collision.collisionSnake(29, 3, snakeXY, 9, 1));
        }
    }
}
