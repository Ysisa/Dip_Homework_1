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
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(33, 43);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 0;
            this.open.Text = "打开图像";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(33, 72);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "保存图像";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(33, 101);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 2;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pixel
            // 
            this.pixel.Location = new System.Drawing.Point(33, 130);
            this.pixel.Name = "pixel";
            this.pixel.Size = new System.Drawing.Size(75, 23);
            this.pixel.TabIndex = 3;
            this.pixel.Text = "像素法";
            this.pixel.UseVisualStyleBackColor = true;
            this.pixel.Click += new System.EventHandler(this.pixel_Click);
            // 
            // memory
            // 
            this.memory.Location = new System.Drawing.Point(33, 159);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(75, 23);
            this.memory.TabIndex = 4;
            this.memory.Text = "内存法";
            this.memory.UseVisualStyleBackColor = true;
            this.memory.Click += new System.EventHandler(this.memory_Click);
            // 
            // zoom
            // 
            this.zoom.Location = new System.Drawing.Point(33, 188);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(75, 23);
            this.zoom.TabIndex = 5;
            this.zoom.Text = "缩放";
            this.zoom.UseVisualStyleBackColor = true;
            this.zoom.Click += new System.EventHandler(this.zoom_Click);
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.pixel);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.open);
            this.Name = "Picture";
            this.Text = "Picture";
            this.Load += new System.EventHandler(this.Picture_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Picture_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button pixel;
        private System.Windows.Forms.Button memory;
        private System.Windows.Forms.Button zoom;
    }
}