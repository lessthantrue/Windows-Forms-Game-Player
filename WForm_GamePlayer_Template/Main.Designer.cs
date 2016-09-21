using System.Windows.Forms.GamePlayer;
using WForm_GamePlayer;

namespace $safeprojectname$
{
    partial class Main
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
            this.m_gameControl = new GamePlayerControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_gameControl)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox1
            // 
            this.m_gameControl.Location = new System.Drawing.Point(0, 0);
            this.m_gameControl.Name = "m_gameControl";
            this.m_gameControl.Size = new System.Drawing.Size(500, 500);
            this.m_gameControl.TabIndex = 0;
            this.m_gameControl.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.m_gameControl);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.m_gameControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GamePlayerControl m_gameControl;
    }
}

