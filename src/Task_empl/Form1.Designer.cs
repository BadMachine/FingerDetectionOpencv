namespace Task_empl
{
    partial class Task
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
            this.Viewport = new System.Windows.Forms.PictureBox();
            this.OpenImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).BeginInit();
            this.SuspendLayout();
            // 
            // Viewport
            // 
            this.Viewport.Location = new System.Drawing.Point(12, 12);
            this.Viewport.Name = "Viewport";
            this.Viewport.Size = new System.Drawing.Size(640, 480);
            this.Viewport.TabIndex = 0;
            this.Viewport.TabStop = false;
            // 
            // OpenImage
            // 
            this.OpenImage.Location = new System.Drawing.Point(734, 12);
            this.OpenImage.Name = "OpenImage";
            this.OpenImage.Size = new System.Drawing.Size(136, 34);
            this.OpenImage.TabIndex = 1;
            this.OpenImage.Text = "Open image";
            this.OpenImage.UseVisualStyleBackColor = true;
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
         
            // 
            // Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 579);
            this.Controls.Add(this.OpenImage);
            this.Controls.Add(this.Viewport);
            this.Name = "Task";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Viewport;
        private System.Windows.Forms.Button OpenImage;

    }
}

