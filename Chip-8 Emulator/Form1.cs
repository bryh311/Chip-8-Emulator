using System.Media;

namespace Chip_8_Emulator
{
    public partial class Form1 : Form
    {
        public static bool stopped = false;
        private Chip8 chip8 { get; set; }
        private int x = 0;
        public Form1()
        {
            InitializeComponent();
            this.chip8 = new Chip8();
            gameTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gameWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush black = new SolidBrush(Color.Black);
            Brush white = new SolidBrush(Color.White);

            g.FillRectangle(black, 0, 0, gameWindow.Width, gameWindow.Height);

            for (int col = 0; col < 64; col++)
            {
                for (int row = 0; row < 32; row++)
                {
                    if (this.chip8.screen[row, col])
                    {
                        g.FillRectangle(white, col * 10, row * 10, 10, 10);
                    }
                }
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gameWindow.Invalidate();
            this.chip8.DecrementTimers();

        }

        private void instructionTimer_Tick(object sender, EventArgs e)
        {
            this.chip8.Interpret(instructionTimer);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = ofd.FileName;
                this.chip8.Load(filename);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            instructionTimer.Start();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            instructionTimer.Stop();
            this.chip8.Reset();

        }

        private void IsKeyDown(object sender, KeyEventArgs e)
        {
            this.chip8.HandleKeyboardDown(e);
        }

        private void IsKeyUp(object sender, KeyEventArgs e)
        {
            this.chip8.HandleKeyboardUp(e);
        }

        private void shiftBox_Changed(object sender, EventArgs e)
        {
            this.chip8.shiftOption = !this.chip8.shiftOption;
        }

        private void incrementBox_Changed(object sender, EventArgs e)
        {
            this.chip8.incrementOption = !this.chip8.incrementOption;
        }

        private void jumpBox_Changed(object sender, EventArgs e)
        {
            this.chip8.jumpOption = !this.chip8.jumpOption;
        }
    }
}