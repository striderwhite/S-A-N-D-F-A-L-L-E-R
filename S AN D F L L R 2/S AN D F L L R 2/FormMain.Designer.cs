namespace S_A_N_D_F_L_L_R_2
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonSand = new System.Windows.Forms.Button();
            this.buttonWater = new System.Windows.Forms.Button();
            this.buttonEraser = new System.Windows.Forms.Button();
            this.buttonWall = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.timerMouseClicks = new System.Windows.Forms.Timer(this.components);
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.trackBarBrushSize = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSandSpawner = new System.Windows.Forms.Button();
            this.buttonWaterSpawner = new System.Windows.Forms.Button();
            this.buttonFireSpawner = new System.Windows.Forms.Button();
            this.buttonFire = new System.Windows.Forms.Button();
            this.buttonDirt = new System.Windows.Forms.Button();
            this.buttonLava = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonTree = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSand
            // 
            this.buttonSand.Location = new System.Drawing.Point(12, 12);
            this.buttonSand.Name = "buttonSand";
            this.buttonSand.Size = new System.Drawing.Size(75, 23);
            this.buttonSand.TabIndex = 0;
            this.buttonSand.Text = "Sand";
            this.buttonSand.UseVisualStyleBackColor = true;
            this.buttonSand.Click += new System.EventHandler(this.buttonSand_Click);
            // 
            // buttonWater
            // 
            this.buttonWater.Location = new System.Drawing.Point(96, 12);
            this.buttonWater.Name = "buttonWater";
            this.buttonWater.Size = new System.Drawing.Size(75, 23);
            this.buttonWater.TabIndex = 1;
            this.buttonWater.Text = "Water";
            this.buttonWater.UseVisualStyleBackColor = true;
            this.buttonWater.Click += new System.EventHandler(this.buttonWater_Click);
            // 
            // buttonEraser
            // 
            this.buttonEraser.Location = new System.Drawing.Point(177, 41);
            this.buttonEraser.Name = "buttonEraser";
            this.buttonEraser.Size = new System.Drawing.Size(75, 23);
            this.buttonEraser.TabIndex = 2;
            this.buttonEraser.Text = "Eraser";
            this.buttonEraser.UseVisualStyleBackColor = true;
            this.buttonEraser.Click += new System.EventHandler(this.buttonEraser_Click);
            // 
            // buttonWall
            // 
            this.buttonWall.Location = new System.Drawing.Point(177, 12);
            this.buttonWall.Name = "buttonWall";
            this.buttonWall.Size = new System.Drawing.Size(75, 23);
            this.buttonWall.TabIndex = 3;
            this.buttonWall.Text = "Wall";
            this.buttonWall.UseVisualStyleBackColor = true;
            this.buttonWall.Click += new System.EventHandler(this.buttonWall_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(417, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(156, 52);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(498, 160);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(417, 160);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // timerMouseClicks
            // 
            this.timerMouseClicks.Interval = 5;
            this.timerMouseClicks.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 160);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(417, 96);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(156, 49);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // trackBarBrushSize
            // 
            this.trackBarBrushSize.Location = new System.Drawing.Point(15, 100);
            this.trackBarBrushSize.Maximum = 5;
            this.trackBarBrushSize.Minimum = 1;
            this.trackBarBrushSize.Name = "trackBarBrushSize";
            this.trackBarBrushSize.Size = new System.Drawing.Size(153, 45);
            this.trackBarBrushSize.TabIndex = 11;
            this.trackBarBrushSize.Value = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Brush Size";
            // 
            // buttonSandSpawner
            // 
            this.buttonSandSpawner.Location = new System.Drawing.Point(258, 12);
            this.buttonSandSpawner.Name = "buttonSandSpawner";
            this.buttonSandSpawner.Size = new System.Drawing.Size(129, 23);
            this.buttonSandSpawner.TabIndex = 14;
            this.buttonSandSpawner.Text = "Sand Spawner";
            this.buttonSandSpawner.UseVisualStyleBackColor = true;
            this.buttonSandSpawner.Click += new System.EventHandler(this.buttonSandSpawner_Click);
            // 
            // buttonWaterSpawner
            // 
            this.buttonWaterSpawner.Location = new System.Drawing.Point(258, 41);
            this.buttonWaterSpawner.Name = "buttonWaterSpawner";
            this.buttonWaterSpawner.Size = new System.Drawing.Size(129, 23);
            this.buttonWaterSpawner.TabIndex = 15;
            this.buttonWaterSpawner.Text = "Water Spawner";
            this.buttonWaterSpawner.UseVisualStyleBackColor = true;
            this.buttonWaterSpawner.Click += new System.EventHandler(this.buttonWaterSpawner_Click);
            // 
            // buttonFireSpawner
            // 
            this.buttonFireSpawner.Location = new System.Drawing.Point(258, 71);
            this.buttonFireSpawner.Name = "buttonFireSpawner";
            this.buttonFireSpawner.Size = new System.Drawing.Size(129, 23);
            this.buttonFireSpawner.TabIndex = 16;
            this.buttonFireSpawner.Text = "Fire Spawner";
            this.buttonFireSpawner.UseVisualStyleBackColor = true;
            this.buttonFireSpawner.Click += new System.EventHandler(this.buttonFireSpawner_Click);
            // 
            // buttonFire
            // 
            this.buttonFire.Location = new System.Drawing.Point(96, 41);
            this.buttonFire.Name = "buttonFire";
            this.buttonFire.Size = new System.Drawing.Size(75, 23);
            this.buttonFire.TabIndex = 17;
            this.buttonFire.Text = "Fire";
            this.buttonFire.UseVisualStyleBackColor = true;
            this.buttonFire.Click += new System.EventHandler(this.buttonFire_Click);
            // 
            // buttonDirt
            // 
            this.buttonDirt.Location = new System.Drawing.Point(12, 70);
            this.buttonDirt.Name = "buttonDirt";
            this.buttonDirt.Size = new System.Drawing.Size(75, 23);
            this.buttonDirt.TabIndex = 18;
            this.buttonDirt.Text = "Dirt";
            this.buttonDirt.UseVisualStyleBackColor = true;
            this.buttonDirt.Click += new System.EventHandler(this.buttonDirt_Click);
            // 
            // buttonLava
            // 
            this.buttonLava.Location = new System.Drawing.Point(96, 71);
            this.buttonLava.Name = "buttonLava";
            this.buttonLava.Size = new System.Drawing.Size(75, 23);
            this.buttonLava.TabIndex = 19;
            this.buttonLava.Text = "Lava";
            this.buttonLava.UseVisualStyleBackColor = true;
            this.buttonLava.Click += new System.EventHandler(this.buttonLava_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "WORLD";
            this.openFileDialog.FileName = "world";
            this.openFileDialog.Title = "Load a game world";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "WORLD";
            this.saveFileDialog.FileName = "MyWorld";
            this.saveFileDialog.Title = "Choose where to save your world";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // buttonTree
            // 
            this.buttonTree.Location = new System.Drawing.Point(12, 41);
            this.buttonTree.Name = "buttonTree";
            this.buttonTree.Size = new System.Drawing.Size(75, 23);
            this.buttonTree.TabIndex = 20;
            this.buttonTree.Text = "Tree";
            this.buttonTree.UseVisualStyleBackColor = true;
            this.buttonTree.Click += new System.EventHandler(this.buttonTree_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 194);
            this.Controls.Add(this.buttonTree);
            this.Controls.Add(this.buttonLava);
            this.Controls.Add(this.buttonDirt);
            this.Controls.Add(this.buttonFire);
            this.Controls.Add(this.buttonFireSpawner);
            this.Controls.Add(this.buttonWaterSpawner);
            this.Controls.Add(this.buttonSandSpawner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarBrushSize);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonWall);
            this.Controls.Add(this.buttonEraser);
            this.Controls.Add(this.buttonWater);
            this.Controls.Add(this.buttonSand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.Text = "S A N D F L L R 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSand;
        private System.Windows.Forms.Button buttonWater;
        private System.Windows.Forms.Button buttonEraser;
        private System.Windows.Forms.Button buttonWall;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Timer timerMouseClicks;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TrackBar trackBarBrushSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSandSpawner;
        private System.Windows.Forms.Button buttonWaterSpawner;
        private System.Windows.Forms.Button buttonFireSpawner;
        private System.Windows.Forms.Button buttonFire;
        private System.Windows.Forms.Button buttonDirt;
        private System.Windows.Forms.Button buttonLava;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonTree;
    }
}

