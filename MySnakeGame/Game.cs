using SnakeGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySnakeGame
{
    public partial class Game : Form
    {
        private List<Circle> snake = new List<Circle>();
        private Circle food = new Circle();
        public Game()
        {
            InitializeComponent();
            new Setting();

            gameTimer.Interval = 1000 / Setting.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            StartGame();

        }
        private void StartGame()
        {
            lblGameOver.Visible = false;
            new Setting();

            snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            snake.Add(head);

            lblScore.Text = Setting.Score.ToString();

            GenerateFood();
        }
        public void GenerateFood()
        {
            int maxXPos = pbCanvs.Size.Width / Setting.Width;
            int maxYPos = pbCanvs.Size.Height / Setting.Height;

            Random random = new Random();
            food = new Circle
            {
                X = random.Next(0, maxXPos),
                Y = random.Next(0, maxYPos)
            };

        }
        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Setting.GameOver)
            {
                if (Input.keyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.keyPressed(Keys.Right) && Setting.direction != Direction.Left)
                    Setting.direction = Direction.Right;
                else if (Input.keyPressed(Keys.Left) && Setting.direction != Direction.Right)
                    Setting.direction = Direction.Left;
                else if (Input.keyPressed(Keys.Up) && Setting.direction != Direction.Down)
                    Setting.direction = Direction.Up;
                else if (Input.keyPressed(Keys.Down) && Setting.direction != Direction.Up)
                    Setting.direction = Direction.Down;

                MovePlayer();
            }
            pbCanvs.Invalidate();
        }

        private void MovePlayer()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Setting.direction)
                    {
                        case Direction.Right:
                            snake[i].X++;
                            break;
                        case Direction.Left:
                            snake[i].X--;
                            break;
                        case Direction.Down:
                            snake[i].Y++;
                            break;
                        case Direction.Up:
                            snake[i].Y--;
                            break;
                    }
                    int maxXPos = pbCanvs.Size.Width / Setting.Width;
                    int maxYPos = pbCanvs.Size.Height / Setting.Height;
                    if (snake[i].X < 0 || snake[i].Y < 0 ||
                        snake[i].X >= maxXPos || snake[i].Y >= maxYPos)
                    {
                        Die();
                    }
                    for (int j = 1; j < snake.Count; j++)
                    {
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            Die();
                        }
                    }

                    if (snake[0].X == food.X && snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;

                }
            }
        }
        private void Eat()
        {
            Circle circle = new Circle
            {

                X = snake[snake.Count - 1].X,
                Y = snake[snake.Count - 1].Y


            };
            snake.Add(circle);

            Setting.Score += Setting.Points;
            lblScore.Text = Setting.Score.ToString();
            GenerateFood();
        }
        private void Die()
        {
            Setting.GameOver = true;
        }

        private void pbCanvs_Paint(object sender, PaintEventArgs e)
        {
            Graphics canavs = e.Graphics;
            if (!Setting.GameOver)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i == 0)
                        snakeColour = Brushes.Black;
                    else
                        snakeColour = Brushes.Red;

                    canavs.FillEllipse(snakeColour,
                        new Rectangle(snake[i].X * Setting.Width,
                        snake[i].Y * Setting.Height,
                        Setting.Width, Setting.Height));
                    canavs.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Setting.Width,
                        food.Y * Setting.Height, Setting.Width, Setting.Height));
                }
            }
            else
            {
                string gameOver = "Game over \n Your Final score is: " + Setting.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);

        }
    }
}
