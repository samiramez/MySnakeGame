﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    public class Setting
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }

        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }

        public Setting()
        {
            Width = 16;
            Height = 16;
            Speed = 12;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Direction.Down;
        }

    }
}
