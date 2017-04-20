using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace S_A_N_D_F_L_L_R_2
{
    public partial class FormMain : Form
    {
        public static bool _Run = true;        //game loop flag
        public static bool _Pause = false;
        public static bool _Boarders = false;
        public static Thread _ThreadGameLoop;
        public static CDrawer _Canvas;
        public static int scale = 8;
        public static int xsize = Screen.PrimaryScreen.Bounds.Width-350;
        public static int ysize = Screen.PrimaryScreen.Bounds.Height - 350;
        //public static int xsize = 750;
        //public static int ysize = 500;
        public static int maxX = xsize / scale;
        public static int maxY = ysize / scale;
        public static Type[,] sandArray;
        public static Type _Selected_Type = Type.sand;
        public static bool[,] updated;
        public static Random rand;
        public static Stopwatch stopwatch;
        public static int sleep_delay;
        public const int frame_rate = 30;
        public static int frame_count = 0;
        public static int max_fill = (maxX * maxY) * (2 / 3);
        public static Point last_coords = new Point(0, 0);
        //public enum Type { wall, sand, water, empty, custom, sandspawner, firespawner };
        public enum Dir { up, down, left, right, downright, downleft, upleft, upright }

        public FormMain()
        {
            InitializeComponent(); 
        }

        private void GameLoop()
        {
            //init things...
            _Canvas = new CDrawer(xsize, ysize,false,true);
            _Canvas.Scale = scale;
            sandArray = new Type[maxX + 1, maxY + 1];
            updated = new bool[maxX , maxY];
            rand = new Random();
            stopwatch = new Stopwatch();
            //MakeSand();
            stopwatch.Start();

            while (_Run)
            {
                if (_Pause == true)
                    continue;
                stopwatch.Reset();
                stopwatch.Start();
                for (int x = 1; x < maxX - 1; x++)
                {
                    for (int y = 1; y < maxY - 1; y++)
                    {
                        if (updated[x, y])
                            continue;
                        switch(sandArray[x,y])
                        {
                            case Type.sand:
                            {
                                MoveSand(x, y);   //return false if didnt move (and therefore didnt call draw)
                                break;
                            }
                            case Type.water:
                            {
                                MoveWater(x, y);   //return false if didnt move (and therefore didnt call draw)
                                break;
                            }
                            case Type.fire:
                            {
                                MoveFire(x, y);
                                break;
                            }
                            case Type.dirt:
                                MoveDirt(x, y);
                                break;
                            case Type.lava:
                                MoveLava(x, y);
                                break;
                            case Type.tree:
                                MoveTree(x, y);
                                break;
                            case Type.sandspawner:
                            case Type.firespawner:
                            case Type.waterspawner:
                            {
                                Spawner(sandArray[x,y],x, y);
                                break;
                            }
                        }

                    }
                }
                for (int x = 1; x < maxX - 1; x++)
                {
                    for (int y = 1; y < maxY - 1; y++)
                    {
                        Draw(sandArray[x,y], x, y);
                    }
                }
                frame_count = 0;
                updated = new bool[maxX, maxY];
                Thread.Sleep(CalcSleepDelay());
                _Canvas.Clear();
                _Canvas.Render();
            }
        }

     

        private int CalcSleepDelay()
        {
            int calc = (int)(frame_rate - stopwatch.ElapsedMilliseconds);
            if(calc < 0)
            {
                return 1;
            }
            else
            {
                return calc;
            }
        }

        private void MakeSand()
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (rand.Next(0, 1000) < 100)
                    {
                        sandArray[x, y] = Type.sand;
                    }
                    else
                    {
                        sandArray[x, y] = Type.empty;
                    }
                }
            }
        }

        private void Spawner(Type type,int x, int y)
        {
            switch(type)
            {
                case Type.sandspawner:
                {
                    sandArray[x, y + 1] = Type.sand;
                    break;
                }
                case Type.waterspawner:
                {
                    sandArray[x, y + 1] = Type.water;
                    break;
                }
                case Type.firespawner:
                {
                    if(rand.Next(1000)<250)
                    {
                        sandArray[x, y + 1] = Type.fire;
                    }
                    break;
                }
            }

        }

        private bool MoveWater(int x, int y)
        {
            bool CanMoveDown = IsEmpty(x, y + 1);
            bool CanMoveDownLeft = IsEmpty(x - 1, y + 1);
            bool CanMoveDownRight = IsEmpty(x + 1, y + 1);
            bool CanMoveRight = IsEmpty(x + 1, y);
            bool CanMoveLeft = IsEmpty(x - 1, y);

            //special case where below is lava, turn into "wall"
            if (sandArray[x, y + 1] == Type.lava)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x, y + 1] = Type.wall;
                //Move(x, y, x, y + 1);
                updated[x, y + 1] = true;
                updated[x, y] = true;
            }


            //special case where below is dirt, turn into "tree"
            if (sandArray[x, y + 1] == Type.dirt)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x, y + 1] = Type.tree;
                //Move(x, y, x, y + 1);
                updated[x, y + 1] = true;
                updated[x, y] = true;
            }

            //special case where above is dirt, turn into "tree"
            if (sandArray[x, y - 1] == Type.dirt)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x, y - 1] = Type.tree;
                //Move(x, y, x, y + 1);
                updated[x, y - 1] = true;
                updated[x, y] = true;
            }

            if (!CanMoveDown && !CanMoveDownLeft && !CanMoveDownRight && !CanMoveLeft && !CanMoveRight)  //if cant move
            {
                return false;
            }
            if (CanMoveDown)
            {
                Move(x, y, x, y + 1);          //move down
                updated[x, y + 1] = true;
                return true;
            }
            if (CanMoveRight && CanMoveLeft)
            {

                if (rand.Next(0, 1000) < 500)         //%50 chance of moving either direction
                {
                    Move(x, y, x + 1, y);   //move right
                    updated[x + 1, y] = true;
                }

                else
                {
                    Move(x, y, x - 1, y);   //move left
                    updated[x - 1, y] = true;
                }
                return true;
            }
            if (CanMoveLeft)
            {
                Move(x, y, x - 1, y);   //move left
                updated[x - 1, y] = true;
                return true;
            }
            if (CanMoveRight)
            {
                Move(x, y, x + 1, y);   //move right
                updated[x + 1, y] = true;
                return true;
            }
            if (CanMoveDownLeft && CanMoveDownRight)
            {
                if (rand.Next(0, 1000) < 500)         //%50 chance of moving either direction
                {
                    Move(x, y, x + 1, y + 1);   //move dwn right
                    updated[x + 1, y + 1] = true;
                }

                else
                {
                    Move(x, y, x - 1, y + 1);   //move dwn left
                    updated[x - 1, y + 1] = true;
                }
                return true;
            }
            if (CanMoveDownLeft)
            {
                Move(x, y, x - 1, y + 1);   //move dwn left
                updated[x - 1, y + 1] = true;
                return true;
            }
            if (CanMoveDownRight)
            {
                Move(x, y, x + 1, y + 1);   //move dwn right
                updated[x + 1, y + 1] = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// "tree" never actually moves, but has special behaviour when touching water
        /// </summary>
        /// <returns></returns>
        private bool MoveTree(int x, int y)
        {
            //check all directions, if touching water turn THAT water into tree (5% chance)
            Type top, topRight, right, bottomRight, bottom, bottomLeft, left, topLeft;
            top = sandArray[x, y - 1];
            topRight = sandArray[x + 1, y - 1];
            right = sandArray[x + 1, y];
            bottomRight = sandArray[x + 1, y + 1];
            bottom = sandArray[x, y + 1];
            bottomLeft = sandArray[x - 1, y + 1];
            left = sandArray[x - 1, y];
            topLeft = sandArray[x - 1, y - 1];

            if(top == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if(rand.Next(0,1000) < 100)
                {
                    sandArray[x, y - 1] = Type.tree;
                    updated[x, y - 1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }

            if (topRight == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x + 1, y - 1] = Type.tree;
                    updated[x + 1, y - 1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }

            
            if (right == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x+1, y] = Type.tree;
                    updated[x+1, y] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }
            if(bottomRight == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x+1, y + 1] = Type.tree;
                    updated[x+1, y + 1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }
            if (bottom == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x, y +1] = Type.tree;
                    updated[x, y +1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }

            if (bottomLeft == Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x -1, y + 1] = Type.tree;
                    updated[x-1, y + 1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }

            if (left==Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x-1, y ] = Type.tree;
                    updated[x-1, y] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }
            
            if(topLeft==Type.water)
            {
                //25% chance THAT gets turn into tree....
                if (rand.Next(0, 1000) < 50)
                {
                    sandArray[x-1, y - 1] = Type.tree;
                    updated[x-1, y -1] = true;
                    updated[x, y] = true;
                    return true;
                }
                return true;
            }
            return true;
        }

        private bool MoveLava(int x, int y)
        {
            bool CanMoveDown = IsEmpty(x, y + 1);
            bool CanMoveDownLeft = IsEmpty(x - 1, y + 1);
            bool CanMoveDownRight = IsEmpty(x + 1, y + 1);
            bool CanMoveLeft = IsEmpty(x - 1, y);
            bool CanMoveRight = IsEmpty(x + 1, y);

            //space case where below is water, turn into "wall"
            if (sandArray[x, y + 1] == Type.water)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x, y + 1] = Type.wall;
                //Move(x, y, x, y + 1);
                updated[x, y + 1] = true;
                updated[x, y] = true;
            }

            //space case where below is on left, turn into "wall"
            if (sandArray[x-1, y] == Type.water)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x - 1, y] = Type.wall;
                //Move(x, y, x, y + 1);
                updated[x - 1, y] = true;
                updated[x, y] = true;
            }

            //space case where below is on right, turn into "wall"
            if (sandArray[x+1, y] == Type.water)
            {
                sandArray[x, y] = Type.empty;
                sandArray[x + 1, y] = Type.wall;
                //Move(x, y, x, y + 1);
                updated[x + 1, y] = true;
                updated[x, y] = true;
            }


            if (!CanMoveDown && !CanMoveDownLeft && !CanMoveDownRight && !CanMoveLeft && !CanMoveRight)  //if cant move
            {
                return false;
            }
            if (CanMoveDown)
            {
                Move(x, y, x, y + 1);          //move down
                updated[x, y + 1] = true;
                return true;
            }
            if (CanMoveRight && CanMoveLeft)
            {

                if (rand.Next(0, 1000) < 500)         //%50 chance of moving either direction
                {
                    Move(x, y, x + 1, y);   //move right
                    updated[x + 1, y] = true;
                }

                else
                {
                    Move(x, y, x - 1, y);   //move left
                    updated[x - 1, y] = true;
                }
                return true;
            }
            if (CanMoveLeft)
            {
                Move(x, y, x - 1, y);   //move left
                updated[x - 1, y] = true;
                return true;
            }
            if (CanMoveRight)
            {
                Move(x, y, x + 1, y);   //move right
                updated[x + 1, y] = true;
                return true;
            }
            if (CanMoveDownLeft && CanMoveDownRight)
            {
                if (rand.Next(0, 1000) < 500)         //%50 chance of moving either direction
                {
                    Move(x, y, x + 1, y + 1);   //move dwn right
                    updated[x + 1, y + 1] = true;
                }

                else
                {
                    Move(x, y, x - 1, y + 1);   //move dwn left
                    updated[x - 1, y + 1] = true;
                }
                return true;
            }
            if (CanMoveDownLeft)
            {
                Move(x, y, x - 1, y + 1);   //move dwn left
                updated[x - 1, y + 1] = true;
                return true;
            }
            if (CanMoveDownRight)
            {
                Move(x, y, x + 1, y + 1);   //move dwn right
                updated[x + 1, y + 1] = true;
                return true;
            }
           
            return false;
        }

        private bool MoveSand(int x, int y)
        {
            bool CanMoveDown = IsEmpty(x, y + 1);
            bool CanMoveDownLeft = IsEmpty(x - 1, y + 1);
            bool CanMoveDownRight = IsEmpty(x + 1, y + 1);

            //space case where below is water...
            if (sandArray[x, y + 1] == Type.water)
            {
                Move(x, y, x, y + 1);
                updated[x, y + 1] = true;
                updated[x, y] = true;
            }

            if (!CanMoveDown && !CanMoveDownLeft && !CanMoveDownRight)  //if cant move
            {
                return false;
            }
            if (CanMoveDown)
            {
                Move(x, y, x, y + 1);          //move down
                updated[x, y + 1] = true;
                return true;
            }
            if (CanMoveDownLeft && CanMoveDownRight)
            {
                if (rand.Next(0, 1000) < 500)         //%50 chance of moving either direction
                {
                    Move(x, y, x + 1, y + 1);   //move dwn right
                    updated[x + 1, y + 1] = true;
                }

                else
                {
                    Move(x, y, x - 1, y + 1);   //move dwn left
                    updated[x - 1, y + 1] = true;
                }
                return true;
            }
            if (CanMoveDownLeft)
            {
                Move(x, y, x - 1, y + 1);   //move dwn left
                updated[x - 1, y + 1] = true;
                return true;
            }
            if (CanMoveDownRight)
            {
                Move(x, y, x + 1, y + 1);   //move dwn right
                updated[x + 1, y + 1] = true;
                return true;
            }
            return false;
        }

        private bool MoveFire(int x, int y)
        {
            //bool canMoveUp = IsEmpty(x, y - 1);
            bool canMoveTopRight = IsEmpty(x + 1, y - 1);
            bool canMoveTopLeft = IsEmpty(x - 1, y - 1);
            if (y == 0 || y == 1)   //delete if at top
                sandArray[x, y] = Type.empty;
            if (!canMoveTopRight && !canMoveTopLeft)
            {
                return false;
            }
            if(canMoveTopLeft && canMoveTopRight)
            {
                if (rand.Next(0,1000)<500)         //%50 chance of moving either direction
                {
                    Move(x, y, x -1, y - 1);  //move top left
                    updated[x - 1, y - 1] = true;
                    return true;
                }
                else
                {
                    Move(x, y, x + 1, y - 1);  //move top left
                    updated[x + 1 , y- 1] = true;
                    return true;
                }
            }
            if (canMoveTopLeft)
            {
                Move(x, y, x - 1, y - 1);  //move top left
                updated[x - 1, y - 1] = true;
                return true;
            }
            if (canMoveTopRight)
            {
                Move(x, y, x +1 , y - 1);  //move top right
                updated[x +1 , y - 1] = true;
                return true;
            }
            return false;
        }

        private bool MoveDirt(int x,int y)
        {
            bool canMoveDown = IsEmpty(x, y + 1);
            if(canMoveDown)
            {
                Move(x, y, x, y + 1);
                updated[x, y +1] = true;
                return true;
            }
            return false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (_ThreadGameLoop != null)
            {
                _ThreadGameLoop.Abort();
                _Canvas.Close();
            }
            //Launch thread
            _ThreadGameLoop = new Thread(GameLoop);
            _ThreadGameLoop.Start();
            timerMouseClicks.Start();
        }

        /// <summary>
        /// this just pulls for mouse clicks and spawns sand if their holding the mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            Point p;
            if(_Canvas != null && _Canvas.GetLastMouseLeftClickScaled(out p))
            {
                for(int i = 0; i < trackBarBrushSize.Value; i++)
                {
                    try
                    {
                        sandArray[p.X, p.Y] = _Selected_Type;
                        sandArray[p.X + i, p.Y] = _Selected_Type;
                        sandArray[p.X, p.Y + i] = _Selected_Type;
                        sandArray[p.X - i, p.Y] = _Selected_Type;
                        sandArray[p.X, p.Y - i] = _Selected_Type;
                        sandArray[p.X + i, p.Y + i] = _Selected_Type;
                        sandArray[p.X - i, p.Y + i] = _Selected_Type;
                        sandArray[p.X - i, p.Y - i] = _Selected_Type;
                        sandArray[p.X + i, p.Y - i] = _Selected_Type;
                    }
                    catch
                    {
                        //lol
                    }
                }
                last_coords = p;
            }
            if (_Canvas != null  && _Canvas.GetMouseDown())
            {
                _Canvas.GetLastMousePositionScaled(out last_coords);
                for (int i = 0; i < trackBarBrushSize.Value; i++)
                {
                    try
                    {
                        //v shit way to draw a bunch of sand. throws when trying to draw off screen
                        sandArray[last_coords.X, last_coords.Y] = _Selected_Type;
                        sandArray[last_coords.X + i, last_coords.Y] = _Selected_Type;
                        sandArray[last_coords.X, last_coords.Y + i] = _Selected_Type;
                        sandArray[last_coords.X - i, last_coords.Y] = _Selected_Type;
                        sandArray[last_coords.X, last_coords.Y - i] = _Selected_Type;
                        sandArray[last_coords.X + i, last_coords.Y + i] = _Selected_Type;
                        sandArray[last_coords.X - i, last_coords.Y + i] = _Selected_Type;
                        sandArray[last_coords.X - i, last_coords.Y - i] = _Selected_Type;
                        sandArray[last_coords.X + i, last_coords.Y - i] = _Selected_Type;
                    }
                    catch
                    {
                        //lol
                    }
                }
            }
        }

        /// <summary>
        /// swaps sand 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public static new void Move(int x1, int y1, int x2, int y2)
        {
            Type oldType = sandArray[x2, y2];
            sandArray[x2, y2] = sandArray[x1, y1];
            sandArray[x1, y1] = oldType;
            return;
        }

        public static void Draw(Type type, int x, int y)
        {
            if (type == Type.sand)
            {
                    _Canvas.AddRectangle(x, y, 1, 1, Color.Yellow, 2, Color.Black);
            }
            if (type == Type.wall)
            {
                _Canvas.AddRectangle(x, y, 1, 1, Color.Gray, 1, Color.Black);
            }

            if (type == Type.water)
            {
                if (rand.Next(0,1000)<500)
                    _Canvas.AddRectangle(x, y, 1, 1, Color.Blue, 1, Color.CornflowerBlue);
                else
                    _Canvas.AddRectangle(x, y, 1, 1, Color.Blue, 2, Color.CornflowerBlue);
            }

            if (type == Type.fire)
            {
                if(rand.Next(0,1000)<500)
                    _Canvas.AddRectangle(x, y, 1, 1, Color.OrangeRed, 2, Color.OrangeRed);
                else
                    _Canvas.AddRectangle(x, y, 1, 1, Color.OrangeRed, 2, Color.Red);
            }

            if(type == Type.dirt)
            {
                _Canvas.AddRectangle(x, y, 1, 1, Color.SaddleBrown, 1, Color.Black);
            }

            if(type == Type.lava)
            {
                if (rand.Next(0, 1000) < 500)
                    _Canvas.AddRectangle(x, y, 1, 1, Color.Red);
                else
                    _Canvas.AddRectangle(x, y, 1, 1, Color.Orange);
            }

            if (type == Type.tree)
            {
               _Canvas.AddRectangle(x, y, 1, 1, Color.ForestGreen);
            }

            if (type == Type.waterspawner)
            {
                _Canvas.AddRectangle(x, y, 1, 1, Color.Blue, 1, Color.Brown);
            }
            if (type == Type.sandspawner)
            {
                _Canvas.AddRectangle(x, y, 1, 1, Color.Yellow, 1,Color.Brown);
            }
            /////
            if (type == Type.firespawner)
            {
                _Canvas.AddRectangle(x, y, 1, 1, Color.Red, 1, Color.Brown);
            }
            return;
        }

        private static bool IsEmpty(int x, int y)
        { 

            //special case: OOB where y >= y-1
            if (y >= maxY - 1) { return false; }
            return (sandArray[x, y] == Type.empty);

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_ThreadGameLoop != null)
            {
                _ThreadGameLoop.Abort();
                _Canvas.Close();
            }
            //Launch thread
            _ThreadGameLoop = new Thread(GameLoop);
            _ThreadGameLoop.Start();
            timerMouseClicks.Start();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    updated[x, y] = false;
                    sandArray[x, y] = Type.empty;
                }
            }
            _Canvas.Clear();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    //2.5% chance to gen somthing
                    if(rand.Next(0,1000)<25)
                    {
                        //gen random type. Used #8 as max gen enum because I dont want "spawners" to generate in world
                        Array vals = Enum.GetValues(typeof(Type));
                        Type randType = (Type)vals.GetValue(rand.Next(5)); sandArray[x, y + 1] = Type.fire;
                        sandArray[x, y] = randType;
                    }
                }
            }
        }

        private void buttonWater_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Water";
            //panelType.BackColor = Color.Blue;
            _Selected_Type = Type.water;
        }

        private void buttonSand_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Sand";
            //panelType.BackColor = Color.Yellow;
            _Selected_Type = Type.sand;
        }

        private void buttonEraser_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Eraser";
            //panelType.BackColor = Color.White;
            _Selected_Type = Type.empty;
        }

        private void buttonWall_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Wall";
            //panelType.BackColor = Color.Gray;
            _Selected_Type = Type.wall;
        }

        private void buttonSandSpawner_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Sand Spawner";
            //panelType.BackColor = Color.LightYellow;
            _Selected_Type = Type.sandspawner;
        }

        private void buttonWaterSpawner_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Water Spawner";
            //panelType.BackColor = Color.BlueViolet;
            _Selected_Type = Type.waterspawner;
        }

        private void buttonFireSpawner_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Fire Spawner";
            //panelType.BackColor = Color.OrangeRed;
            _Selected_Type = Type.firespawner;
        }

        private void buttonTree_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Tree";
            //panelType.BackColor = Color.Green;
            _Selected_Type = Type.tree;
        }

        private void buttonFire_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Fire";
            //panelType.BackColor = Color.Red;
            _Selected_Type = Type.fire;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ThreadGameLoop.Abort();
        }

        private void buttonDirt_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Dirt";
            //panelType.BackColor = Color.Brown;
            _Selected_Type = Type.dirt;
        }

        private void buttonLava_Click(object sender, EventArgs e)
        {
            //labelType.Text = "Lava";
            //panelType.BackColor = Color.Orange;
            _Selected_Type = Type.lava;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog.ShowDialog();
            string path = "";
            if (dr == DialogResult.Cancel)
                return;
            if( dr== DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }
            //ok we get the file... now 
            try
            {
                FileStream inFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryFormatter deseralizer = new BinaryFormatter();

                sandArray = (Type[,]) deseralizer.Deserialize(inFileStream);      
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cant load file... \n" + ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //show dialog to get a save path, then serialize current world
            try
            {
                //_Pause = true;
                DialogResult dr = saveFileDialog.ShowDialog();
                string fileName = "oops.WORLD";
                if (dr == DialogResult.Cancel)
                    _Pause = false;
                    return;
                if(dr == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    _Pause = false;
                }


                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter seralizer = new BinaryFormatter();

                seralizer.Serialize(fileStream, sandArray);
                //_Pause = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to load world...\n"+ex.Message);
            }
            
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }


    }
}
