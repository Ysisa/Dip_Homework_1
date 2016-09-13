namespace Picture
{
    partial class Zoom
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nearestNeigh = new System.Windows.Forms.RadioButton();
            this.Text1 = new System.Windows.Forms.Label();
            this.xZoom = new System.Windows.Forms.TextBox();
            this.yZoom = new System.Windows.Forms.TextBox();
            this.startZoom = new System.Windows.Forms.Button();
            this.Text2 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nearestNeigh);
            this.groupBox1.Location = new System.Drawing.Point(28, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "灰度插值";
            // 
            // nearestNeigh
            // 
            this.nearestNeigh.AutoSize = true;
            this.nearestNeigh.Checked = true;
            this.nearestNeigh.Location = new System.Drawing.Point(26, 41);
            this.nearestNeigh.Name = "nearestNeigh";
            this.nearestNeigh.Size = new System.Drawing.Size(71, 16);
            this.nearestNeigh.TabIndex = 0;
            this.nearestNeigh.TabStop = true;
            this.nearestNeigh.Text = "临近插值";
            this.nearestNeigh.UseVisualStyleBackColor = true;
            // 
            // Text1
            // 
            this.Text1.AutoSize = true;
            this.Text1.Location = new System.Drawing.Point(60, 149);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(53, 12);
            this.Text1.TabIndex = 1;
            this.Text1.Text = "横向像素";
            this.Text1.Click += new System.EventHandler(this.Text_Click);
            // 
            // xZoom
            // 
            this.xZoom.Location = new System.Drawing.Point(62, 176);
            this.xZoom.Name = "xZoom";
            this.xZoom.Size = new System.Drawing.Size(63, 21);
            this.xZoom.TabIndex = 3;
            this.xZoom.Text = "1";
            // 
            // yZoom
            // 
            this.yZoom.Location = new System.Drawing.Point(254, 176);
            this.yZoom.Name = "yZoom";
            this.yZoom.Size = new System.Drawing.Size(63, 21);
            this.yZoom.TabIndex = 4;
            this.yZoom.Text = "1";
            this.yZoom.TextChanged += new System.EventHandler(this.yZoom_TextChanged);
            // 
            // startZoom
            // 
            this.startZoom.Location = new System.Drawing.Point(93, 227);
            this.startZoom.Name = "startZoom";
            this.startZoom.Size = new System.Drawing.Size(75, 23);
            this.startZoom.TabIndex = 5;
            this.startZoom.Text = "确定";
            this.startZoom.UseVisualStyleBackColor = true;
            this.startZoom.Click += new System.EventHandler(this.startZoom_Click);
            // 
            // Text2
            // 
            this.Text2.AutoSize = true;
            this.Text2.Location = new System.Drawing.Point(252, 149);
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(53, 12);
            this.Text2.TabIndex = 6;
            this.Text2.Text = "纵向像素";
            this.Text2.Click += new System.EventHandler(this.Text2_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(195, 227);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 7;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // Zoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 262);
            this.ControlBox = false;
            this.Controls.Add(this.close);
            this.Controls.Add(this.Text2);
            this.Controls.Add(this.startZoom);
            this.Controls.Add(this.yZoom);
            this.Controls.Add(this.xZoom);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Zoom";
            this.Text = "图像缩放";
            this.Load += new System.EventHandler(this.Zoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton nearestNeigh;
        private System.Windows.Forms.Label Text1;
        private System.Windows.Forms.TextBox xZoom;
        private System.Windows.Forms.TextBox yZoom;
        private System.Windows.Forms.Button startZoom;
        private System.Windows.Forms.Label Text2;
        private System.Windows.Forms.Button close;
    }
}