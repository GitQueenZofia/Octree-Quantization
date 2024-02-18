namespace gk3
{
    partial class gk3
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
            pictureBox = new PictureBox();
            loadImageButton = new Button();
            next10Button = new Button();
            next20Button = new Button();
            groupBox = new GroupBox();
            gen2Button = new Button();
            gen1Button = new Button();
            edgesButton = new CheckBox();
            animationLabel = new Label();
            radio50 = new RadioButton();
            radio20 = new RadioButton();
            radio10 = new RadioButton();
            animationButton = new Button();
            color = new Label();
            next50Button = new Button();
            newImageBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newImageBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Control;
            pictureBox.Location = new Point(0, -1);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(447, 596);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // loadImageButton
            // 
            loadImageButton.Location = new Point(6, 20);
            loadImageButton.Name = "loadImageButton";
            loadImageButton.Size = new Size(146, 34);
            loadImageButton.TabIndex = 1;
            loadImageButton.Text = "Load Image";
            loadImageButton.UseVisualStyleBackColor = true;
            loadImageButton.Click += loadImageButton_Click;
            // 
            // next10Button
            // 
            next10Button.Location = new Point(6, 113);
            next10Button.Name = "next10Button";
            next10Button.Size = new Size(146, 34);
            next10Button.TabIndex = 2;
            next10Button.Text = "-10";
            next10Button.UseVisualStyleBackColor = true;
            next10Button.Click += next10Button_Click;
            // 
            // next20Button
            // 
            next20Button.Location = new Point(8, 153);
            next20Button.Name = "next20Button";
            next20Button.Size = new Size(144, 34);
            next20Button.TabIndex = 3;
            next20Button.Text = "-20";
            next20Button.UseVisualStyleBackColor = true;
            next20Button.Click += next20Button_Click;
            // 
            // groupBox
            // 
            groupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox.Controls.Add(gen2Button);
            groupBox.Controls.Add(gen1Button);
            groupBox.Controls.Add(edgesButton);
            groupBox.Controls.Add(animationLabel);
            groupBox.Controls.Add(radio50);
            groupBox.Controls.Add(radio20);
            groupBox.Controls.Add(radio10);
            groupBox.Controls.Add(animationButton);
            groupBox.Controls.Add(color);
            groupBox.Controls.Add(next50Button);
            groupBox.Controls.Add(loadImageButton);
            groupBox.Controls.Add(next20Button);
            groupBox.Controls.Add(next10Button);
            groupBox.Location = new Point(934, 12);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(160, 620);
            groupBox.TabIndex = 4;
            groupBox.TabStop = false;
            // 
            // gen2Button
            // 
            gen2Button.Location = new Point(2, 552);
            gen2Button.Name = "gen2Button";
            gen2Button.Size = new Size(158, 34);
            gen2Button.TabIndex = 15;
            gen2Button.Text = "generate 2";
            gen2Button.UseVisualStyleBackColor = true;
            gen2Button.Click += gen2Button_Click;
            // 
            // gen1Button
            // 
            gen1Button.Location = new Point(0, 509);
            gen1Button.Name = "gen1Button";
            gen1Button.Size = new Size(160, 34);
            gen1Button.TabIndex = 14;
            gen1Button.Text = "generate 1";
            gen1Button.UseVisualStyleBackColor = true;
            gen1Button.Click += gen1Button_Click;
            // 
            // edgesButton
            // 
            edgesButton.AutoSize = true;
            edgesButton.Location = new Point(6, 255);
            edgesButton.Name = "edgesButton";
            edgesButton.Size = new Size(132, 29);
            edgesButton.TabIndex = 13;
            edgesButton.Text = "Draw edges";
            edgesButton.UseVisualStyleBackColor = true;
            edgesButton.CheckedChanged += edgesButton_CheckedChanged;
            // 
            // animationLabel
            // 
            animationLabel.AutoSize = true;
            animationLabel.Location = new Point(8, 310);
            animationLabel.Name = "animationLabel";
            animationLabel.Size = new Size(94, 25);
            animationLabel.TabIndex = 12;
            animationLabel.Text = "Animation";
            // 
            // radio50
            // 
            radio50.AutoSize = true;
            radio50.Location = new Point(8, 464);
            radio50.Name = "radio50";
            radio50.Size = new Size(64, 29);
            radio50.TabIndex = 11;
            radio50.Text = "-50";
            radio50.UseVisualStyleBackColor = true;
            radio50.CheckedChanged += radio50_CheckedChanged;
            // 
            // radio20
            // 
            radio20.AutoSize = true;
            radio20.Location = new Point(8, 429);
            radio20.Name = "radio20";
            radio20.Size = new Size(64, 29);
            radio20.TabIndex = 10;
            radio20.Text = "-20";
            radio20.UseVisualStyleBackColor = true;
            radio20.CheckedChanged += radio20_CheckedChanged;
            // 
            // radio10
            // 
            radio10.AutoSize = true;
            radio10.Checked = true;
            radio10.Location = new Point(8, 394);
            radio10.Name = "radio10";
            radio10.Size = new Size(64, 29);
            radio10.TabIndex = 9;
            radio10.TabStop = true;
            radio10.Text = "-10";
            radio10.UseVisualStyleBackColor = true;
            radio10.CheckedChanged += radio10_CheckedChanged;
            // 
            // animationButton
            // 
            animationButton.Location = new Point(8, 338);
            animationButton.Name = "animationButton";
            animationButton.Size = new Size(50, 50);
            animationButton.TabIndex = 7;
            animationButton.UseVisualStyleBackColor = true;
            animationButton.Click += animationButton_Click;
            // 
            // color
            // 
            color.AutoSize = true;
            color.Location = new Point(6, 85);
            color.Name = "color";
            color.Size = new Size(144, 25);
            color.TabIndex = 6;
            color.Text = "Color count: 600";
            color.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // next50Button
            // 
            next50Button.Location = new Point(8, 193);
            next50Button.Name = "next50Button";
            next50Button.Size = new Size(142, 34);
            next50Button.TabIndex = 5;
            next50Button.Text = "-50";
            next50Button.UseVisualStyleBackColor = true;
            next50Button.Click += next50Button_Click;
            // 
            // newImageBox
            // 
            newImageBox.Anchor = AnchorStyles.Top;
            newImageBox.BackColor = SystemColors.Control;
            newImageBox.Location = new Point(453, -1);
            newImageBox.Name = "newImageBox";
            newImageBox.Size = new Size(475, 596);
            newImageBox.TabIndex = 5;
            newImageBox.TabStop = false;
            // 
            // gk3
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1108, 644);
            Controls.Add(newImageBox);
            Controls.Add(groupBox);
            Controls.Add(pictureBox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "gk3";
            Text = "Octree";
            WindowState = FormWindowState.Maximized;
            SizeChanged += gk3_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)newImageBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private Button loadImageButton;
        private Button next10Button;
        private Button next20Button;
        private GroupBox groupBox;
        private Label color;
        private Button next50Button;
        private PictureBox newImageBox;
        private RadioButton radio50;
        private RadioButton radio20;
        private RadioButton radio10;
        private Button animationButton;
        private Label animationLabel;
        private CheckBox edgesButton;
        private Button gen2Button;
        private Button gen1Button;
    }
}