using System;
using System.Threading;
using System.IO;

namespace ce103_hw5_snake_functions
{
    public class ce103snakegame
    {
        public const int SNAKE_ARRAY_SIZE = 310;
        public const ConsoleKey UP_ARROW = ConsoleKey.UpArrow;
        public const ConsoleKey LEFT_ARROW = ConsoleKey.LeftArrow;
        public const ConsoleKey RIGHT_ARROW = ConsoleKey.RightArrow;
        public const ConsoleKey DOWN_ARROW = ConsoleKey.DownArrow;
        public const ConsoleKey ENTER_KEY = ConsoleKey.Enter;
        public const ConsoleKey EXIT_BUTTON = ConsoleKey.Escape; // ESC
        public const ConsoleKey PAUSE_BUTTON = ConsoleKey.P; //p
        const char SNAKE_HEAD = (char)125;
        const char SNAKE_BODY = (char)62;
        const char WALL = (char)219;
        const char FOOD = (char)64;
        const char BLANK = ' ';


        public ConsoleKey waitForAnyKey()
        {
            ConsoleKey pressed;

            while (!Console.KeyAvailable) ;

            pressed = Console.ReadKey(false).Key;
            //pressed = tolower(pressed);
            return pressed;
        }

        public int gamespeed()
        {
            int snkspeed = 10;
            Console.Clear();
            Console.Write("Select the game speed 1 - 9 and press enter: ");
            int speedchocie = Convert.ToUInt16(Console.ReadLine());
            switch (speedchocie)
            {
                case 1:
                    snkspeed = 45;
                    break;
                case 2:
                    snkspeed = 40;
                    break;
                case 3:
                    snkspeed = 35;
                    break;
                case 4:
                    snkspeed = 30;
                    break;
                case 5:
                    snkspeed = 25;
                    break;
                case 6:
                    snkspeed = 20;
                    break;
                case 7:
                    snkspeed = 15;
                    break;
                case 8:
                    snkspeed = 10;
                    break;
                case 9:
                    snkspeed = 5;
                    break;

            }
            return snkspeed;
        }

        public void pauseMenu()
        {

            Console.SetCursorPosition(28, 23);
            Console.Write("--- Stopped ---");

            waitForAnyKey();
            Console.SetCursorPosition(28, 23);
            Console.Write("            ");
            return;
        }

        //This function checks if a key has press, then checks if its any of the arrow keys/ p/esc key. It changes direction acording to the key press.
        public ConsoleKey checkKeyspress(ConsoleKey direction)
        {
            ConsoleKey cprss;

            if (Console.KeyAvailable == true) //If a key has been press
            {
                cprss = Console.ReadKey(false).Key;
                if (direction != cprss)
                {
                    if (cprss == DOWN_ARROW && direction != UP_ARROW)
                    {
                        direction = cprss;
                    }
                    else if (cprss == UP_ARROW && direction != DOWN_ARROW)
                    {
                        direction = cprss;
                    }
                    else if (cprss == LEFT_ARROW && direction != RIGHT_ARROW)
                    {
                        direction = cprss;
                    }
                    else if (cprss == RIGHT_ARROW && direction != LEFT_ARROW)
                    {
                        direction = cprss;
                    }
                    else if (cprss == EXIT_BUTTON || cprss == PAUSE_BUTTON)
                    {
                        pauseMenu();
                    }
                }
            }
            return (direction);
        }
        //Cycles around checking if the x y coordinates ='s the snake coordinates as one of this parts
        //One thing to note, a snake of length 4 cannot collide with itself, therefore there is no need to call this function when the snakes length is <= 4
        public bool collisionSnake(int x, int y, int[,] snakeloc, int snakesize, int dtct)
        {
            int b;
            for (b = dtct; b < snakesize; b++) //Checks if the snake collided with itself
            {
                if (x == snakeloc[0, b] && y == snakeloc[1, b])
                    return true;
            }
            return false;
        }
        //Generates food & Makes sure the food doesn't appear on top of the snake <- This sometimes causes a lag issue!!! Not too much of a problem tho
        public void generateFood(int[] foodloc, int breadth, int size, int[,] snakeloc, int snakesize)
        {
            Random RandomNumbers = new Random();
            do
            {
                //RandomNumbers.Seed(time(null));
                foodloc[0] = RandomNumbers.Next() % (breadth - 2) + 2;
                //RandomNumbers.Seed(time(null));
                foodloc[1] = RandomNumbers.Next() % (size - 6) + 2;
            } while (collisionSnake(foodloc[0], foodloc[1], snakeloc, snakesize, 0)); //This should prevent the "Food" from being created on top of the snake. - However the food has a chance to be created ontop of the snake, in which case the snake should eat it...

            Console.SetCursorPosition(foodloc[0], foodloc[1]);
            Console.Write(FOOD);
        }

        /*
        Moves the snake array forward, i.e. 
        This:
         x 1 2 3 4 5 6
         y 1 1 1 1 1 1
        Becomes This:
         x 1 1 2 3 4 5
         y 1 1 1 1 1 1

         Then depending on the direction (in this case west - left) it becomes:

         x 0 1 2 3 4 5
         y 1 1 1 1 1 1

         snakeXY[0][0]--; <- if direction left, take 1 away from the x coordinate
        */
        public void moveSnakeArray(int[,] snakeloc, int snakesize, ConsoleKey direction)
        {
            int a;
            for (a = snakesize - 1; a >= 1; a--)
            {
                snakeloc[0, a] = snakeloc[0, a - 1];
                snakeloc[1, a] = snakeloc[1, a - 1];
            }

            /*
            because we dont actually know the new snakes head x y, 
            we have to check the direction and add or take from it depending on the direction.
            */
            switch (direction)
            {
                case DOWN_ARROW:
                    snakeloc[1, 0]++;
                    break;
                case RIGHT_ARROW:
                    snakeloc[0, 0]++;
                    break;
                case UP_ARROW:
                    snakeloc[1, 0]--;
                    break;
                case LEFT_ARROW:
                    snakeloc[0, 0]--;
                    break;
            }

            return;
        }

        /**
        *
        *	  @name   Move Snake Body (move)
        *
        *	  @brief Move snake body
        *
        *	  Moving snake body
        *
        *	  @param  [in] snakeXY [\b int[,]]  snake coordinates
        *	  
        *	  @param  [in] snakeLength [\b int]  index of fibonacci number in the serie
        *	  
        *	  @param  [in] direction [\b ConsoleKey]  index of fibonacci number in the serie
        **/
        public void move(int[,] snakeloc, int snakesize, ConsoleKey direction)
        {
            int a;
            int b;

            //Remove the tail ( HAS TO BE DONE BEFORE THE ARRAY IS MOVED!!!!! )
            a = snakeloc[0, snakesize - 1];
            b = snakeloc[1, snakesize - 1];

            Console.SetCursorPosition(a, b);
            Console.Write(BLANK);

            //Changes the head of the snake to a body part
            Console.SetCursorPosition(snakeloc[0, 0], snakeloc[1, 0]);
            Console.Write(SNAKE_BODY);

            moveSnakeArray(snakeloc, snakesize, direction);

            Console.SetCursorPosition(snakeloc[0, 0], snakeloc[1, 0]);
            Console.Write(SNAKE_HEAD);

            Console.SetCursorPosition(1, 1); //Gets rid of the darn flashing underscore.

            return;
        }


        /**
        *
        *	  @name   eatfood (eat)
        *
        *	  @brief Snake eat food
        *
        *	  Eating @
        *
        *	  @param  [in] snakeXY [\b int[,]]  snake coordinates
        *	  
        *	  @param  [in] foodXY [\b int]  index of fibonacci number in the serie
        *	  
        *	  
        **/
        //This function checks if the snakes head his on top of the food, if it is then it'll generate some more food...
        public bool eatFood(int[,] snakeloc, int[] foodloc)
        {
            if (snakeloc[0, 0] == foodloc[0] && snakeloc[1, 0] == foodloc[1])
            {
                foodloc[0] = 0;
                foodloc[1] = 0; //This should prevent a nasty bug (loops) need to check if the bug still exists...

                return true;
            }

            return false;
        }


        /**
        *
        *	  @name   Collision Detection (console)
        *
        *	  @brief Detection of collision
        *
        *	  Collision Detection
        *
        *	  @param  [in] snakeXY [\b int[,]]  snake coordinates
        *	  
        *	  @param  [in] consoleWidth [\b int]  console witdh
        *	  
        *	  @param  [in] snakeLength [\b int]  snake length
        **/

        public bool collisionDetection(int[,] snakeloc, int cslwidth, int cslheight, int snakesize) //Need to Clean this up a bit
        {
            bool clsn = false;
            if ((snakeloc[0, 0] == 1) || (snakeloc[1, 0] == 1) || (snakeloc[0, 0] == cslwidth) || (snakeloc[1, 0] == cslheight - 4)) //Checks if the snake collided wit the wall or it's self
                clsn = true;
            else
                if (collisionSnake(snakeloc[0, 0], snakeloc[1, 0], snakeloc, snakesize, 1)) //If the snake collided with the wall, theres no point in checking if it collided with itself.
                clsn = true;

            return (clsn);
        }


        /**
        *
        *	  @name   Refresh Bar (refresh)
        *
        *	  @brief Refresh menu
        *
        *	  Refresh bar
        *
        *	  @param  [in] score [\b int]  snake coordinates
        *	  
        *	  @param  [in] speed [\b int]  index of fibonacci number in the serie
        *	  
        *	  
        **/
        public void refreshInfoBar(int gamescore, int gamespeed)
        {
            Console.SetCursorPosition(5, 24);
            Console.Write("Game Score: " + gamescore);

            Console.SetCursorPosition(5, 26);
            Console.Write("Game Speed: " + gamespeed);

            Console.SetCursorPosition(55, 26);
            Console.Write("Coder: Can Yavuz");

            Console.SetCursorPosition(55, 24);
            Console.Write("App Version: 1.0");

            return;
        }

        public void createHighScores()
        {
            TextWriter file = new StreamWriter(@"..\\..\\..\\highscores.txt");
            int i;
            if (file == null)
            {
                Console.Write("FAILED TO CREATE HIGHSCORES!!! EXITING!");
                Environment.Exit(0);
            }
            for (i = 0; i < 5; i++)
            {
                file.Write(i + 1);
                file.Write("\t0\t\t\tEMPTY\n");
            }
            file.Flush();
            file.Close();

            return;
        }

        /**
        *
        *	  @name   youWinScreen (win)
        *
        *	  @brief Win screen
        *
        *	  Win Screen
        *
        *	  
        **/

        public void youWinScreen()
        {
            int x = 6, y = 7;
            Console.SetCursorPosition(x, y++);
            Console.Write("'##:::'##::'#######::'##::::'##::::'##:::::'##:'####:'##::: ##:'####:");
            Console.SetCursorPosition(x, y++);
            Console.Write(". ##:'##::'##.... ##: ##:::: ##:::: ##:'##: ##:. ##:: ###:: ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.Write(":. ####::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ####: ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.Write("::. ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ## ## ##:: ##::");
            Console.SetCursorPosition(x, y++);
            Console.Write("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##. ####::..:::");
            Console.SetCursorPosition(x, y++);
            Console.Write("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##:. ###:'####:");
            Console.SetCursorPosition(x, y++);
            Console.Write("::: ##::::. #######::. #######:::::. ###. ###::'####: ##::. ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.Write(":::..::::::.......::::.......:::::::...::...:::....::..::::..::....::");
            Console.SetCursorPosition(x, y++);

            waitForAnyKey();
            Console.Clear(); //clear the console
            return;
        }


        /**
        *
        *	  @name   Game Over Screen 
        *
        *	  @brief Game Over Screen
        *
        *	  Game Over Screen
        *
        *	 
        **/
        public void gameOverScreen()
        {
            int x = 17, y = 3;

            //http://www.network-science.de/ascii/ <- Ascii Art Gen

            Console.SetCursorPosition(x, y++);
            Console.Write(":'######::::::'###::::'##::::'##:'########:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write("'##... ##::::'## ##::: ###::'###: ##.....::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##:::..::::'##:. ##:: ####'####: ##:::::::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##::'####:'##:::. ##: ## ### ##: ######:::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##::: ##:: #########: ##. #: ##: ##...::::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##::: ##:: ##.... ##: ##:.:: ##: ##:::::::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(". ######::: ##:::: ##: ##:::: ##: ########:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(":......::::..:::::..::..:::::..::........::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(":'#######::'##::::'##:'########:'########::'####:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write("'##.... ##: ##:::: ##: ##.....:: ##.... ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##:::: ##: ##:::: ##: ##::::::: ##:::: ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##:::: ##: ##:::: ##: ######::: ########::: ##::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##:::: ##:. ##:: ##:: ##...:::: ##.. ##::::..:::\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(" ##:::: ##::. ## ##::: ##::::::: ##::. ##::'####:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(". #######::::. ###:::: ########: ##:::. ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.Write(":.......::::::...:::::........::..:::::..::....::\n");

            waitForAnyKey();
            Console.Clear(); //clear the console
            return;
        }

        /**
        *
        *	  @name   Start Game (start)
        *
        *	  @brief Start Game 
        *
        *	  Collision Detection
        *
        *	  @param  [in] snakeXY [\b int[,]]  snake coordinates
        *	  
        *	  @param  [in] consoleWidth [\b int]  console witdh
        *	  
        *	  @param  [in] consoleHeight [\b int]  console Height
        *	  
        *	  @param  [in] snakeLength [\b int]  snake length
        *	   
        *	  @param  [in] direction [\b Console Key]  direction
        *	   
        *
        *	   
        *	  @param  [in] score [\b int]  score
        *	   
        *	  @param  [in] speed [\b int]  speed
        **/



        //Messy, need to clean this function up
        public void startGame(int[,] snakeloc, int[] foodloc, int cslwidth, int cslheight, int snakesize, ConsoleKey direction, int gamescore, int gamespeed)
        {
            bool gameOver = false;
            ConsoleKey predirection = ConsoleKey.NoName;
            bool canChangeDirection = true;
            int gameOver2 = 1;
            do
            {
                if (canChangeDirection)
                {
                    predirection = direction;
                    direction = checkKeyspress(direction);
                }

                if (predirection != direction)//Temp fix to prevent the snake from colliding with itself
                    canChangeDirection = false;

                if (true) //haha, it moves according to how fast the computer running it is...
                {
                    //Console.SetCursorPosition(1,1);
                    //Console.Write("%d - %d",clock() , endWait);
                    move(snakeloc, snakesize, direction);
                    canChangeDirection = true;


                    if (eatFood(snakeloc, foodloc))
                    {
                        generateFood(foodloc, cslwidth, cslheight, snakeloc, snakesize); //Generate More Food
                        snakesize++;
                        switch (gamespeed)
                        {
                            case 90:
                                gamescore += 5;
                                break;
                            case 80:
                                gamescore += 7;
                                break;
                            case 70:
                                gamescore += 9;
                                break;
                            case 60:
                                gamescore += 12;
                                break;
                            case 50:
                                gamescore += 15;
                                break;
                            case 40:
                                gamescore += 20;
                                break;
                            case 30:
                                gamescore += 23;
                                break;
                            case 20:
                                gamescore += 25;
                                break;
                            case 10:
                                gamescore += 30;
                                break;
                        }

                        refreshInfoBar(gamescore, gamespeed);
                    }
                    Thread.Sleep(gamespeed);
                }

                gameOver = collisionDetection(snakeloc, cslwidth, cslheight, snakesize);

                if (snakesize >= SNAKE_ARRAY_SIZE - 5) //Just to make sure it doesn't get longer then the array size & crash
                {
                    gameOver2 = 2;//You Win! <- doesn't seem to work - NEED TO FIX/TEST THIS
                    gamescore += 1500; //When you win you get an extra 1500 points!!!
                }

            } while (!gameOver);

            switch (gameOver2)
            {
                case 1:
                    gameOverScreen();

                    break;
                case 2:
                    youWinScreen();
                    break;
            }



            return;
        }

        /**
        *
        *	  @name   Load Environment (environment)
        *
        *	  @brief Load environment
        *
        *	  Load Environment
        *
        *	  @param  [in] consoleWitdh [\b int]  consoleWitdh
        *	  
        *	  @param  [in] consoleHeight [\b int]  consoleHeight
        *	  
        *	  
        **/
        public void loadEnviroment(int cslbreadth, int clsheight)//This can be done in a better way... FIX ME!!!! Also i think it doesn't work properly in ubuntu <- Fixed
        {

            int a = 1, b = 1;
            int rctgheight = clsheight - 4;
            Console.Clear(); //clear the console

            Console.SetCursorPosition(a, b); //Top left corner

            for (; b < rctgheight; b++)
            {
                Console.SetCursorPosition(a, b); //Left Wall 
                Console.Write("#", WALL);

                Console.SetCursorPosition(cslbreadth, b); //Right Wall
                Console.Write("#", WALL);
            }

            b = 1;
            for (; a < cslbreadth + 1; a++)
            {
                Console.SetCursorPosition(a, b); //Left Wall 
                Console.Write("#", WALL);

                Console.SetCursorPosition(a, rctgheight); //Right Wall
                Console.Write("#", WALL);
            }


            return;
        }

        /**
        *
        *	  @name   Load Snake (Snake)
        *
        *	  @brief Load Snake
        *
        *	  Load Environment
        *
        *	  @param  [in] snakeXY [\b int]  snakeXY
        *	  
        *	  @param  [in] snakeLength [\b int]  snakeLength
        *	  
        *	  
        **/
        public void loadSnake(int[,] snakeloc, int snakesize)
        {
            int a;
            /*
            First off, The snake doesn't actually have enough XY coordinates (only 1 - the starting location), thus we use
            these XY coordinates to "create" the other coordinates. For this we can actually use the function used to move the snake.
            This helps create a "whole" snake instead of one "dot", when someone starts a game.
            */
            //moveSnakeArray(snakeXY, snakeLength); //One thing to note ATM, the snake starts of one coordinate to whatever direction it's pointing...

            //This should print out a snake :P
            for (a = 0; a < snakesize; a++)
            {
                Console.SetCursorPosition(snakeloc[0, a], snakeloc[1, a]);
                Console.Write("%c", SNAKE_BODY); //Meh, at some point I should make it so the snake starts off with a head...
            }

            return;
        }

        /* NOTE, This function will only work if the snakes starting direction is left!!!! 
        Well it will work, but the results wont be the ones expected.. I need to fix this at some point.. */

        /**
        *
        *	  @name   prepairSnakeArray (prepair snake)
        *
        *	  @brief Prepair Snake Array
        *
        *	  Prepair Snake Array
        *
        *	  @param  [in] snakeXY [\b int]  snakeXY
        *	  
        *	  @param  [in] snakeLength [\b int]  snakeLength
        *	  
        *	  
        **/
        public void prepairSnakeArray(int[,] snakeloc, int snakesize)
        {
            int i;
            int snakexloc = snakeloc[0, 0];
            int snakeyloc = snakeloc[1, 0];

            // this is used in the function move.. should maybe create a function for it...



            for (i = 1; i <= snakesize; i++)
            {
                snakeloc[0, i] = snakexloc + i;
                snakeloc[1, i] = snakeyloc;
            }

            return;
        }

        /**
        *
        *	  @name   Load Game (Load Game)
        *
        *	  @brief Load Game
        *
        *	  Load Game
        *
        *	  
        **/
        //This function loads the enviroment, snake, etc
        public void loadGame()
        {
            int[,] snakeloc = new int[2, SNAKE_ARRAY_SIZE]; //Two Dimentional Array, the first array is for the X coordinates and the second array for the Y coordinates

            int snakesize = 4; //Starting Length

            ConsoleKey direction = ConsoleKey.LeftArrow; //DO NOT CHANGE THIS TO RIGHT ARROW, THE GAME WILL INSTANTLY BE OVER IF YOU DO!!! <- Unless the prepairSnakeArray function is changed to take into account the direction....

            int[] foodloc = { 5, 5 };// Stores the location of the food

            int gamescore = 0;
            //int level = 1;

            //Window Width * Height - at some point find a way to get the actual dimensions of the console... <- Also somethings that display dont take this dimentions into account.. need to fix this...
            int clswidth = 80;
            int cslheight = 25;

            int gamespdd = gamespeed();

            //The starting location of the snake
            snakeloc[0, 0] = 40;
            snakeloc[1, 0] = 10;

            loadEnviroment(clswidth, cslheight); //borders
            prepairSnakeArray(snakeloc, snakesize);
            loadSnake(snakeloc, snakesize);
            generateFood(foodloc, clswidth, cslheight, snakeloc, snakesize);
            refreshInfoBar(gamescore, gamespdd); //Bottom info bar. Score, Level etc
            startGame(snakeloc, foodloc, clswidth, cslheight, snakesize, direction, gamescore, gamespdd);

            return;
        }

        //**************MENU STUFF**************//

        /**
        *
        *	  @name   menuSelector (menuSelector)
        *
        *	  @brief menu Selector
        *
        *	  Menu Selector
        *
        *	  @param  [in] a [\b int]  a
        *	  
        *	  @param  [in] b [\b int]  b
        *	  
        *	  @param  [in] yStart [\b int]  yStart
        *	  
        *	  
        **/
        public int menuSelector(int a, int b, int yStart)
        {
            char push;
            int x = 0;
            a = a - 2;
            Console.SetCursorPosition(a, yStart);

            Console.Write(">");

            Console.SetCursorPosition(1, 1);


            do
            {
                push = (char)waitForAnyKey();
                //Console.Write("%c %d", key, (int)key);
                if (push == (char)UP_ARROW)
                {
                    Console.SetCursorPosition(a, yStart + x);
                    Console.Write(" ");

                    if (yStart >= yStart + x)
                        x = b - yStart - 2;
                    else
                        x--;
                    Console.SetCursorPosition(a, yStart + x);
                    Console.Write(">");
                }
                else
                    if (push == (char)DOWN_ARROW)
                {
                    Console.SetCursorPosition(a, yStart + x);
                    Console.Write(" ");

                    if (x + 2 >= b - yStart)
                        x = 0;
                    else
                        x++;
                    Console.SetCursorPosition(a, yStart + x);
                    Console.Write(">");
                }
                //Console.SetCursorPosition(1,1);
                //Console.Write("%d", key);
            } while (push != (char)ENTER_KEY); //While doesn't equal enter... (13 ASCII code for enter) - note ubuntu is 10
            return (x);
        }

        /**
        *
        *	  @name   Welcome Art (Welcome Art)
        *
        *	  @brief Welcome Art
        *
        *	  Welcome Art
        *
        *	  
        **/
        public void welcomeArt()
        {
            Console.Clear(); //clear the console
                             //Ascii art reference: http://www.chris.com/ascii/index.php?art=animals/reptiles/snakes
            Console.Write("\n");
            Console.Write("\t\t    _________         _________ 			\n");
            Console.Write("\t\t   /         \\       /         \\ 			\n");
            Console.Write("\t\t  /  /~~~~~\\  \\     /  /~~~~~\\  \\ 			\n");
            Console.Write("\t\t  |  |     |  |     |  |     |  | 			\n");
            Console.Write("\t\t  |  |     |  |     |  |     |  | 			\n");
            Console.Write("\t\t  |  |     |  |     |  |     |  |         /	\n");
            Console.Write("\t\t  |  |     |  |     |  |     |  |       //	\n");
            Console.Write("\t\t (o  o)    \\  \\_____/  /     \\  \\_____/ / 	\n");
            Console.Write("\t\t  \\__/      \\         /       \\        / 	\n");
            Console.Write("\t\t    |        ~~~~~~~~~         ~~~~~~~~ 		\n");
            Console.Write("\t\t    ^											\n");
            Console.Write("\t		Welcome To The Snake Game!			\n");
            Console.Write("\t			    Press Any Key To Continue...	\n");
            Console.Write("\n");

            waitForAnyKey();
            return;
        }

        /**
        *
        *	  @name   Controls (Controls)
        *
        *	  @brief Controls of game
        *
        *	  Game Control
        *
        *	  
        **/
        public void controls()
        {
            int x = 10, y = 5;
            Console.Clear(); //clear the console
            Console.SetCursorPosition(x, y++);
            Console.Write("Controls\n");
            Console.SetCursorPosition(x++, y++);
            Console.Write("Use the following arrow keys to direct the snake to the food: ");
            Console.SetCursorPosition(x, y++);
            Console.Write("Right Arrow");
            Console.SetCursorPosition(x, y++);
            Console.Write("Left Arrow");
            Console.SetCursorPosition(x, y++);
            Console.Write("Top Arrow");
            Console.SetCursorPosition(x, y++);
            Console.Write("Bottom Arrow");
            Console.SetCursorPosition(x, y++);
            Console.SetCursorPosition(x, y++);
            Console.Write("P & Esc pauses the game.");
            Console.SetCursorPosition(x, y++);
            Console.SetCursorPosition(x, y++);
            Console.Write("Press any key to continue...");
            waitForAnyKey();
            return;
        }

        /**
        *
        *	  @name   exitXY (exit)
        *
        *	  @brief Exit from game
        *
        *	  Exit Game
        *
        *	  
        **/
        public void exitYN()
        {
            char push;
            Console.SetCursorPosition(9, 8);
            Console.Write("Are you sure you want to exit(Y/N)\n");

            do
            {
                push = (char)waitForAnyKey();
                push = char.ToLower(push);
            } while (!(push == 'y' || push == 'n'));

            if (push == 'y')
            {
                Console.Clear(); //clear the console
                Environment.Exit(1);
            }
            return;
        }

        /**
        *
        *	  @name  Main Menu (Main Menu)
        *
        *	  @brief Main Menu
        *
        *	  Main Menu
        *
        *	  
        **/
        public int mainMenu()
        {
            int q = 10, w = 5;
            int qw = w;

            int choice;

            Console.Clear(); //clear the console
                             //Might be better with arrays of strings???
            Console.SetCursorPosition(q, w++);
            Console.Write("New Game\n");
            Console.SetCursorPosition(q, w++);
            Console.Write("High Scores\n");
            Console.SetCursorPosition(q, w++);
            Console.Write("Controls\n");
            Console.SetCursorPosition(q, w++);
            Console.Write("Exit\n");
            Console.SetCursorPosition(q, w++);

            choice = menuSelector(q, w, qw);

            return (choice);
        }

        //**************END MENU STUFF**************//





    }
}
