namespace Picture
{
    partial class Picture
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
            this.open = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.pixel = new System.Windows.Forms.Button();
            this.memory = new System.Windows.Forms.Button();
            this.zoom = new System.Windows.Forms.Button();
            this.To128Color = new System.Windows.Forms.Button();
            this.To32Color = new System.Windows.Forms.Button();
            this.To8Color = new System.Windows.Forms.Button();
            this.To4Color = new System.Windows.Forms.Button();
            this.To2Color = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(33, 43);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(89, 23);
            this.open.TabIndex = 0;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(33, 72);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(89, 23);
            this.save.TabIndex = 1;
            this.save.Text = "保存图像";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(33, 101);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(89, 23);
            this.close.TabIndex = 2;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pixel
            // 
            this.pixel.Location = new System.Drawing.Point(33, 130);
            this.pixel.Name = "pixel";
            this.pixel.Size = new System.Drawing.Size(89, 23);
            this.pixel.TabIndex = 3;
            this.pixel.Text = "黑白化(像素)";
            this.pixel.UseVisualStyleBackColor = true;
            this.pixel.Click += new System.EventHandler(this.pixel_Click);
            // 
            // memory
            // 
            this.memory.Location = new System.Drawing.Point(33, 159);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(89, 23);
            this.memory.TabIndex = 4;
            this.memory.Text = "黑白化(内存)";
            this.memory.UseVisualStyleBackColor = true;
            this.memory.Click += new System.EventHandler(this.memory_Click);
            // 
            // zoom
            // 
            this.zoom.Location = new System.Drawing.Point(33, 188);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(89, 23);
            this.zoom.TabIndex = 5;
            this.zoom.Text = "缩放";
            this.zoom.UseVisualStyleBackColor = true;
            this.zoom.Click += new System.EventHandler(this.zoom_Click);
            // 
            // To128Color
            // 
            this.To128Color.Location = new System.Drawing.Point(33, 217);
            this.To128Color.Name = "To128Color";
            this.To128Color.Size = new System.Drawing.Size(89, 23);
            this.To128Color.TabIndex = 6;
            this.To128Color.Text = "128级";
            this.To128Color.UseVisualStyleBackColor = true;
            this.To128Color.Click += new System.EventHandler(this.To128Color_Click);
            // 
            // To32Color
            // 
            this.To32Color.Location = new System.Drawing.Point(33, 246);
            this.To32Color.Name = "To32Color";
            this.To32Color.Size = new System.Drawing.Size(89, 23);
            this.To32Color.TabIndex = 7;
            this.To32Color.Text = "32级";
            this.To32Color.UseVisualStyleBackColor = true;
            this.To32Color.Click += new System.EventHandler(this.To32Color_Click);
            // 
            // To8Color
            // 
            this.To8Color.Location = new System.Drawing.Point(33, 275);
            this.To8Color.Name = "To8Color";
            this.To8Color.Size = new System.Drawing.Size(89, 23);
            this.To8Color.TabIndex = 8;
            this.To8Color.Text = "8级";
            this.To8Color.UseVisualStyleBackColor = true;
            this.To8Color.Click += new System.EventHandler(this.To8Color_Click);
            // 
            // To4Color
            // 
            this.To4Color.Location = new System.Drawing.Point(33, 304);
            this.To4Color.Name = "To4Color";
            this.To4Color.Size = new System.Drawing.Size(89, 23);
            this.To4Color.TabIndex = 9;
            this.To4Color.Text = "4级";
            this.To4Color.UseVisualStyleBackColor = true;
            this.To4Color.Click += new System.EventHandler(this.To4Color_Click);
            // 
            // To2Color
            // 
            this.To2Color.Location = new System.Drawing.Point(33, 333);
            this.To2Color.Name = "To2Color";
            this.To2Color.Size = new System.Drawing.Size(89, 23);
            this.To2Color.TabIndex = 10;
            this.To2Color.Text = "2级";
            this.To2Color.UseVisualStyleBackColor = true;
            this.To2Color.Click += new System.EventHandler(this.To2Color_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(129, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(643, 507);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.To2Color);
            this.Controls.Add(this.To4Color);
            this.Controls.Add(this.To8Color);
            this.Controls.Add(this.To32Color);
            this.Controls.Add(this.To128Color);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.pixel);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.open);
            this.Name = "Picture";
            this.Text = "Picture";
            this.Load += new System.EventHandler(this.Picture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button pixel;
        private System.Windows.Forms.Button memory;
        private System.Windows.Forms.Button zoom;
        private System.Windows.Forms.Button To128Color;
        private System.Windows.Forms.Button To32Color;
        private System.Windows.Forms.Button To8Color;
        private System.Windows.Forms.Button To4Color;
        private System.Windows.Forms.Button To2Color;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}