namespace Picture
{
    partial class Show
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
            this.showPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.showPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // showPicture
            // 
            this.showPicture.Location = new System.Drawing.Point(0, 0);
            this.showPicture.Name = "showPicture";
            this.showPicture.Size = new System.Drawing.Size(100, 50);
            this.showPicture.TabIndex = 0;
            this.showPicture.TabStop = false;
            // 
            // Show
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.showPicture);
            this.Name = "Show";
            this.Text = "Show";
            ((System.ComponentModel.ISupportInitialize)(this.showPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox showPicture;
    }
}