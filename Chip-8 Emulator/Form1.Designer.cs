namespace Chip_8_Emulator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            loadButton = new NonSelectableButton();
            resetButton = new NonSelectableButton();
            gameWindow = new PictureBox();
            startButton = new NonSelectableButton();
            gameTimer = new System.Windows.Forms.Timer(components);
            instructionTimer = new System.Windows.Forms.Timer(components);
            shiftOption = new Label();
            incrementOption = new Label();
            modernJumpLabel = new Label();
            shiftBox = new NonSelectableCheck();
            incrementBox = new NonSelectableCheck();
            jumpCheck = new NonSelectableCheck();
            delayBox = new NonSelectableTextBox();
            delayNumber = new Label();
            ((System.ComponentModel.ISupportInitialize)gameWindow).BeginInit();
            SuspendLayout();
            // 
            // loadButton
            // 
            loadButton.Location = new Point(306, 2);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(75, 23);
            loadButton.TabIndex = 1;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // resetButton
            // 
            resetButton.Location = new Point(387, 2);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(75, 23);
            resetButton.TabIndex = 2;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // gameWindow
            // 
            gameWindow.BackColor = Color.Black;
            gameWindow.Location = new Point(12, 28);
            gameWindow.Margin = new Padding(3, 2, 3, 2);
            gameWindow.Name = "gameWindow";
            gameWindow.Size = new Size(640, 320);
            gameWindow.TabIndex = 3;
            gameWindow.TabStop = false;
            gameWindow.Paint += gameWindow_Paint;
            // 
            // startButton
            // 
            startButton.Location = new Point(219, 2);
            startButton.Margin = new Padding(3, 2, 3, 2);
            startButton.Name = "startButton";
            startButton.Size = new Size(82, 22);
            startButton.TabIndex = 4;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 16;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // instructionTimer
            // 
            instructionTimer.Interval = 1;
            instructionTimer.Tick += instructionTimer_Tick;
            // 
            // shiftOption
            // 
            shiftOption.AutoSize = true;
            shiftOption.Location = new Point(673, 74);
            shiftOption.Name = "shiftOption";
            shiftOption.Size = new Size(116, 15);
            shiftOption.TabIndex = 5;
            shiftOption.Text = "Modern Shift Option";
            shiftOption.Click += shiftOption_Click;
            // 
            // incrementOption
            // 
            incrementOption.AutoSize = true;
            incrementOption.Location = new Point(673, 160);
            incrementOption.Name = "incrementOption";
            incrementOption.Size = new Size(123, 15);
            incrementOption.TabIndex = 6;
            incrementOption.Text = "Old Increment Option";
            // 
            // modernJumpLabel
            // 
            modernJumpLabel.AutoSize = true;
            modernJumpLabel.Location = new Point(673, 257);
            modernJumpLabel.Name = "modernJumpLabel";
            modernJumpLabel.Size = new Size(121, 15);
            modernJumpLabel.TabIndex = 7;
            modernJumpLabel.Text = "Modern Jump Option";
            // 
            // shiftBox
            // 
            shiftBox.AutoSize = true;
            shiftBox.Location = new Point(724, 122);
            shiftBox.Name = "shiftBox";
            shiftBox.Size = new Size(15, 14);
            shiftBox.TabIndex = 8;
            shiftBox.UseVisualStyleBackColor = true;
            shiftBox.CheckedChanged += shiftBox_Changed;
            // 
            // incrementBox
            // 
            incrementBox.AutoSize = true;
            incrementBox.Location = new Point(724, 204);
            incrementBox.Name = "incrementBox";
            incrementBox.Size = new Size(15, 14);
            incrementBox.TabIndex = 9;
            incrementBox.UseVisualStyleBackColor = true;
            incrementBox.CheckedChanged += incrementBox_Changed;
            // 
            // jumpCheck
            // 
            jumpCheck.AutoSize = true;
            jumpCheck.Location = new Point(724, 286);
            jumpCheck.Name = "jumpCheck";
            jumpCheck.Size = new Size(15, 14);
            jumpCheck.TabIndex = 10;
            jumpCheck.UseVisualStyleBackColor = true;
            jumpCheck.CheckedChanged += jumpBox_Changed;
            // 
            // delayBox
            // 
            delayBox.Location = new Point(673, 342);
            delayBox.Name = "delayBox";
            delayBox.Size = new Size(100, 23);
            delayBox.TabIndex = 11;
            delayBox.TextChanged += delayBox_TextChanged;
            // 
            // delayNumber
            // 
            delayNumber.AutoSize = true;
            delayNumber.Location = new Point(673, 314);
            delayNumber.Name = "delayNumber";
            delayNumber.Size = new Size(71, 15);
            delayNumber.TabIndex = 12;
            delayNumber.Text = "Delay Speed";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Beige;
            ClientSize = new Size(863, 377);
            Controls.Add(delayNumber);
            Controls.Add(delayBox);
            Controls.Add(jumpCheck);
            Controls.Add(incrementBox);
            Controls.Add(shiftBox);
            Controls.Add(modernJumpLabel);
            Controls.Add(incrementOption);
            Controls.Add(shiftOption);
            Controls.Add(startButton);
            Controls.Add(gameWindow);
            Controls.Add(resetButton);
            Controls.Add(loadButton);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Chip-8";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += IsKeyDown;
            KeyUp += IsKeyUp;
            ((System.ComponentModel.ISupportInitialize)gameWindow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NonSelectableButton loadButton;
        private NonSelectableButton resetButton;
        private NonSelectableButton startButton;
        private PictureBox gameWindow;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer instructionTimer;
        private Label shiftOption;
        private Label incrementOption;
        private Label modernJumpLabel;
        private NonSelectableCheck shiftBox;
        private NonSelectableCheck incrementBox;
        private NonSelectableCheck jumpCheck;
        private NonSelectableTextBox delayBox;
        private Label delayNumber;
    }
}