using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Media;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Chip_8_Emulator
{
    internal class Chip8
    {
        private string filename { get; set; }
        private byte[] rom { get; set; }
        private byte[] ram { get; set; }
        private ushort pc { get; set; }
        private ushort indexRegister { get; set; }
        private Stack<ushort> stack { get; set; }
        private byte[] registers { get; set; }
        private SoundPlayer beep { get; set; }
        private bool stopped;
        private bool[] keyboard { get; set; }
        private Random rand { get; set; }

        public bool[,] screen { get; set; }
        public bool shiftOption { get; set; }
        public bool jumpOption { get; set; }
        public bool incrementOption { get; set; }
        public byte delay { get; set; }
        public byte sound { get; set; }

        public readonly byte[] font = {
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };
        public Chip8()
        {
            this.filename = string.Empty;
            this.ram = new byte[4096];
            this.pc = 0x200;
            this.indexRegister = 0;
            this.stack = new Stack<ushort>();
            this.delay = 0;
            this.sound = 0;
            this.registers = new byte[16];
            string directoryLocation = Directory.GetCurrentDirectory();
            this.beep = new SoundPlayer(Path.Combine(directoryLocation, @"assets\blipSelect.wav"));
            this.stopped = true;
            int offset = 0x050;
            for (int i = 0; i < font.Length; i++)
            {
                this.ram[i + offset] = (byte)(font[i]);
            }

            this.screen = new bool[32, 64];
            for (int col = 0; col < 64; col++)
            {
                for (int row = 0; row < 32; row++)
                {
                    // I am not a fan of this 2d array syntax
                    // Why is it different from JAVA and C and C++ and JavaScript!
                    screen[row, col] = false;
                }
            }

            this.keyboard = new bool[16];
            this.shiftOption = false;
            this.jumpOption = false;
            this.incrementOption = false;

            this.rand = new Random();
        }
        public void Load(string filename)
        {
            this.filename = filename;
            try
            {
                this.rom = File.ReadAllBytes(filename);
                int offset = 0x200;
                for (int i = 0; i < this.rom.Length; i++)
                {
                    this.ram[i + offset] = this.rom[i];
                }
            }
            catch(FileLoadException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Reset()
        {
            this.pc = 0x200;
            for (int row = 0; row < 32; row++)
            {
                for (int col = 0; col < 64; col++)
                {
                    this.screen[row, col] = false;
                }
            }
            for (int i = 0x200; i < this.ram.Length; i++)
            {
                this.ram[i] = 0;
            }
            this.delay = 0;
            this.sound = 0;
            this.stack = new Stack<ushort>();
            this.indexRegister = 0;
            this.stopped = true;
        }

        public void Interpret(System.Windows.Forms.Timer timer)
        {
            if (this.rom == null)
            {
                timer.Stop();
                MessageBox.Show("File not loaded!");
                return;
            }

            ushort instruction = (ushort)((this.ram[this.pc] << 8) + this.ram[this.pc + 1]);
            ushort first = (ushort)(instruction & 0xF000);
            ushort x = (ushort)((instruction & 0x0F00) >> 8);
            ushort y = (ushort)((instruction & 0x00F0) >> 4);
            ushort n = (ushort)(instruction & 0x000F);
            ushort nn = (ushort)(instruction & 0x00FF);
            ushort nnn = (ushort)(instruction & 0x0FFF);
            bool jump = true;

            //Console.WriteLine("X: {0} Y: {1}", x, y);

            switch (first)
            {
                case 0x0000:
                    switch (nnn)
                    {
                        case 0x0E0:
                            for (int row = 0; row < 32; row++)
                            {
                                for (int col = 0; col < 64; col++)
                                {
                                    this.screen[row, col] = false;
                                }
                            }
                            break;
                        case 0x0EE:
                            this.pc = this.stack.Pop();
                            break;
                    }
                    break;
                case 0x1000:
                    this.pc = nnn;
                    jump = false;
                    break;
                case 0x2000:
                    this.stack.Push(this.pc);
                    this.pc = nnn;
                    jump = false;
                    break;
                case 0x3000:
                    if (this.registers[x] == nn)
                    {
                        this.pc += 2;
                    }
                    break;
                case 0x4000:
                    if (this.registers[x] != nn)
                    {
                        this.pc += 2;
                    }
                    break;
                case 0x5000:
                    if (this.registers[x] == this.registers[y])
                    {
                        this.pc += 2;
                    }
                    break;
                case 0x6000:
                    this.registers[x] = (byte) nn;
                    break;
                case 0x7000:
                    this.registers[x] += (byte) nn;
                    break;
                case 0x8000:
                    switch(n)
                    {
                        case 0:
                            this.registers[x] = this.registers[y];
                            break;
                        case 1:
                            this.registers[x] = (byte)(this.registers[x] | this.registers[y]);
                            break;
                        case 2:
                            this.registers[x] = (byte)(this.registers[x] & this.registers[y]);
                            break;
                        case 3:
                            this.registers[x] = (byte)(this.registers[x] ^ this.registers[y]);
                            break;
                        case 4:
                            try
                            {
                                checked
                                {
                                    this.registers[x] = (byte)(this.registers[x] + this.registers[y]);
                                }
                                this.registers[0xF] = 0;
                            }
                            catch (OverflowException e)
                            {
                                unchecked
                                {
                                    this.registers[x] = (byte)(this.registers[x] + this.registers[y]);
                                }
                                this.registers[0xF] = 1;
                            }
                            break;
                        case 5:
                            try
                            {
                                checked
                                {
                                    this.registers[x] = (byte)(this.registers[x] - this.registers[y]);
                                }
                                this.registers[0xF] = 0;
                            }
                            catch (OverflowException e)
                            {
                                unchecked
                                {
                                    this.registers[x] = (byte)(this.registers[x] - this.registers[y]);
                                }
                                this.registers[0xF] = 1;
                            }
                            break;
                        case 6:
                            // I will implement optional instruction later
                            if (this.shiftOption)
                            {
                                this.registers[x] = this.registers[y];
                            }
                            try
                            {
                                checked
                                {
                                    this.registers[x] = (byte)(this.registers[x] >> 1);
                                }
                                this.registers[0xF] = 0;
                            }
                            catch (OverflowException e)
                            {
                                unchecked
                                {
                                    this.registers[x] = (byte)(this.registers[x] >> 1);
                                }
                                this.registers[0xF] = 1;
                            }
                            
                            break;
                        case 7:
                            try
                            {
                                checked
                                {
                                    this.registers[x] = (byte)(this.registers[y] - this.registers[x]);
                                }
                                this.registers[0xF] = 0;
                            }
                            catch (OverflowException e)
                            {
                                unchecked
                                {
                                    this.registers[x] = (byte)(this.registers[y] - this.registers[x]);
                                }
                                this.registers[0xF] = 1;
                            }
                            break;
                        case 0xE:
                            if (this.shiftOption)
                            {
                                this.registers[x] = this.registers[y];
                            }
                            try
                            {
                                checked
                                {
                                    this.registers[x] = (byte)(this.registers[x] << 1);
                                }
                                this.registers[0xF] = 0;
                            }
                            catch (OverflowException e)
                            {
                                unchecked
                                {
                                    this.registers[x] = (byte)(this.registers[x] << 1);
                                }
                                this.registers[0xF] = 1;
                            }
                            break;

                    }
                    break;
                case 0x9000:
                    if (this.registers[x] != this.registers[y])
                    {
                        this.pc += 2;
                    }
                    break;
                case 0xA000:
                    this.indexRegister = nnn;
                    break;
                case 0xB000:
                    if (this.jumpOption)
                    {
                        this.pc = (ushort)(nnn + this.registers[x]);
                    }
                    else
                    {
                        this.pc = (ushort)(nnn+ this.registers[0]);
                    }
                    break;
                case 0xC000:
                    byte num = (byte)rand.Next(0, 255);
                    this.registers[x] = (byte)(num & nn);
                    break;
                case 0xD000:
                    byte xCoord = this.registers[x];
                    //Console.WriteLine("X" + xCoord.ToString());
                    byte yCoord = this.registers[y];
                    //Console.WriteLine("Y" + yCoord.ToString());
                    xCoord %= 64;
                    yCoord %= 32;

                    this.registers[0xF] = 0;
                    byte mask = 1;
                    for (int i = 0; i < n; i++)
                    {
                        if (i + yCoord > 31)
                        {
                            break;
                        }
                        //Console.WriteLine(this.indexRegister);
                        byte spriteData = this.ram[this.indexRegister + i];
                        
                        for (int j = 0; j < 8; j++)
                        {
                            if (j + xCoord > 63)
                            {
                                continue;
                            }
                            byte val = (byte)(mask & (spriteData >> (7 - j)));
                            if (val == 1)
                            {
                                bool cur = this.screen[yCoord + i, xCoord + j];
                                if (cur)
                                {
                                    this.screen[yCoord + i, xCoord + j] = false;
                                    this.registers[0xF] = 1;
                                }
                                else
                                {
                                    this.screen[yCoord + i, xCoord + j] = true;
                                    // Console.WriteLine("draw");
                                }
                                //xCoord++;
                            }
                            //yCoord++;
                            //spriteData  = (byte)(spriteData >> 1);
                        }
                        
                    }
                    break;
                case 0xE000:
                    switch(nn)
                    {
                        case 0x9E:
                            byte key = this.registers[x];
                            if (this.keyboard[key])
                            {
                                this.pc += 2;
                            }
                            break;
                        case 0xA1:
                            byte key2 = this.registers[x];
                            if (!this.keyboard[key2])
                            {
                                this.pc += 2;
                            }
                            break;
                    }
                    break;
                case 0xF000:
                    switch (nn)
                    {
                        case 0x07:
                            this.registers[x] = this.delay;
                            break;
                        case 0x15:
                            this.delay = this.registers[x];
                            break;
                        case 0x18:
                            this.sound = this.registers[x];
                            break; ;
                        case 0x1E:
                            this.indexRegister += this.registers[x];
                            break;
                        case 0x0A:
                            bool pressedFlag = false;
                            for (byte i = 0; i < this.keyboard.Length; i++)
                            {
                                if (keyboard[i])
                                {
                                    this.registers[x] = i;
                                    pressedFlag = true;
                                }
                            }

                            if (!pressedFlag)
                            {
                                this.pc -= 2;
                            }
                            break;
                        case 0x29:
                            int offset = 0x50;
                            byte ch = this.registers[x];
                            this.indexRegister = (ushort)(offset + ch * 5);
                            break;
                        case 0x33:
                            byte convert = this.registers[x];
                            byte num1 = (byte)(convert % 10);
                            byte num2 = (byte)((convert / 10) % 10);
                            byte num3 = (byte)((convert / 100) % 10);
                            this.ram[this.indexRegister] = num3;
                            this.ram[this.indexRegister + 1] = num2;
                            this.ram[this.indexRegister + 2] = num1;
                            break;
                        case 0x55:
                            for (byte i = 0; i <= x; i++)
                            {
                                if (incrementOption)
                                {
                                    this.ram[this.indexRegister] = this.registers[i];
                                    this.indexRegister++;
                                }
                                else
                                {
                                    this.ram[this.indexRegister + i] = this.registers[i];
                                }
                            }
                            break;
                        case 0x65:
                            for (byte i = 0; i <= x; i++)
                            {
                                if (incrementOption)
                                {
                                    this.registers[i] = this.ram[this.indexRegister];
                                    this.indexRegister++;
                                }
                                else
                                {
                                    this.registers[i] = this.ram[this.indexRegister + i];
                                }
                            }
                            break;

                    }
                    break;
                default:
                    Console.WriteLine("Unknow instruction: " + instruction.ToString("X"));
                    break;

            }

            // Console.WriteLine(instruction.ToString("X"));

            if (jump)
            {
                this.pc += 2;
            }
            // Console.WriteLine(this.pc);

            // Console.WriteLine(BitConverter.ToString(this.rom));

        }

        public void DecrementTimers()
        {
            if (this.delay > 0)
            {
                this.delay--;
            }

            if (this.sound > 0)
            {
                this.sound--;
                if (this.stopped)
                {
                    this.beep.PlayLooping();
                    this.stopped = false;
                }
                // Console.WriteLine(this.sound);
                
            }
            else
            {
                this.beep.Stop();
                this.stopped = true;
            }
        }

        // I have no idea if there is a better way to do this
        // there probably is.

        public void HandleKey(KeyEventArgs e, bool downOrUp)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    this.keyboard[1] = downOrUp;
                    break;
                case Keys.D2:
                    this.keyboard[2] = downOrUp;
                    break;
                case Keys.D3:
                    this.keyboard[3] = downOrUp;
                    break;
                case Keys.D4:
                    this.keyboard[12] = downOrUp;
                    break;
                case Keys.Q:
                    this.keyboard[4] = downOrUp;
                    break;
                case Keys.W:
                    this.keyboard[5] = downOrUp;
                    break;
                case Keys.E:
                    this.keyboard[6] = downOrUp;
                    break;
                case Keys.R:
                    this.keyboard[13] = downOrUp;
                    break;
                case Keys.A:
                    this.keyboard[7] = downOrUp;
                    break;
                case Keys.S:
                    this.keyboard[8] = downOrUp;
                    break;
                case Keys.D:
                    this.keyboard[9] = downOrUp;
                    break;
                case Keys.F:
                    this.keyboard[14] = downOrUp;
                    break;
                case Keys.Z:
                    this.keyboard[10] = downOrUp;
                    break;
                case Keys.X:
                    this.keyboard[0] = downOrUp;
                    break;
                case Keys.C:
                    this.keyboard[11] = downOrUp;
                    break;
                case Keys.V:
                    this.keyboard[15] = downOrUp;
                    break;

            }
        }

        public void HandleKeyboardDown(KeyEventArgs e)
        {
            HandleKey(e, true);
        }

        public void HandleKeyboardUp(KeyEventArgs e)
        {
            HandleKey(e, false);
        }
    }
}
