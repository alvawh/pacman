namespace pacman
{
    enum Direction
    {
        North, East, South, West
    }
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x,int y) { X = x; Y = y; }
    }
    public partial class Form1 : Form
    {
        TableLayoutPanel tpanel = new TableLayoutPanel();
        Panel panel;
        Panel[,] panelArray = new Panel[15, 15];
        Position oldPos = new Position(0,0);
        Position newPos;
        Position enemyOld = new Position(14, 14);
        Position enemyNew;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < panelArray.GetLength(0); i++)
            {
                for (int j = 0; j < panelArray.GetLength(0); j++)
                {
                    panel = new Panel();
                    panel.Size = new Size(30, 30);
                    if (i == 0 && j == 0) panel.BackColor = Color.Green;
                    else panel.BackColor = Color.Gray;
                    tpanel.Controls.Add(panel, i, j);
                    panelArray[i, j] = panel;
                }
            }
            Controls.Add(tpanel);
            tpanel.Dock = DockStyle.Fill;
            panelArray[enemyOld.X, enemyOld.Y].BackColor = Color.Red;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) move(Direction.North);
            if (e.KeyCode==Keys.Right) move(Direction.East);
            if (e.KeyCode==Keys.Down) move(Direction.South);
            if (e.KeyCode==Keys.Left) move(Direction.West);
        }
        private void move(Direction d)
        {
            newPos = oldPos;
            switch (d)
            {
                case Direction.North:
                    if (oldPos.Y != 0)
                    {
                        newPos.Y -= 1;
                        break;
                    }
                    else return;

                case Direction.East:
                    if (oldPos.X < panelArray.GetLength(0) -1)
                    {
                        newPos.X += 1;
                        break;
                    }
                    else return;
                   
                case Direction.South:
                    if (oldPos.Y < panelArray.GetLength(0) - 1)
                    {
                        newPos.Y += 1;
                        break;
                    }
                    else return;

                case Direction.West:
                    if (oldPos.X != 0)
                    {
                        newPos.X -= 1;
                        break;
                    }
                    else return;      
            }
            panelArray[oldPos.X, oldPos.Y].BackColor = Color.Gray;
            panelArray[newPos.X, newPos.Y].BackColor = Color.Green;
            oldPos = newPos;
            while (!moveEnemy()) ;
        }
        private bool moveEnemy()
        {
            Random r = new Random();
            Direction d = (Direction)r.Next(4);
            enemyNew = enemyOld;
          
            switch (d)
            {
                case Direction.North:
                    if (enemyOld.Y != 0)
                    {
                        enemyNew.Y -= 1;
                        break;
                    }
                    else return false;

                case Direction.East:
                    if (enemyOld.X < panelArray.GetLength(0) - 1)
                    {
                        enemyNew.X += 1;
                        break;
                    }
                    else return false;

                case Direction.South:
                    if (enemyOld.Y < panelArray.GetLength(0) - 1)
                    {
                        enemyNew.Y += 1;
                        break;
                    }
                    else return false;

                case Direction.West:
                    if (enemyOld.X != 0)
                    {
                        enemyNew.X -= 1;
                        break;
                    }
                    else return false;
            }
            panelArray[enemyOld.X, enemyOld.Y].BackColor = Color.Gray;
            panelArray[enemyNew.X,enemyNew.Y].BackColor = Color.Red;
            enemyOld = enemyNew;
            return true;
        }
    }
}