namespace Ex05_UserInteface
{
    partial class GameSettingsForm
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
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.PlayComputerButton = new System.Windows.Forms.Button();
            this.PlayTwoPlayersButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoardSizeButton.Location = new System.Drawing.Point(54, 28);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(420, 82);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "Board size: 6x6 (Click to increase)";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            this.BoardSizeButton.Click += new System.EventHandler(this.DoWhenClicked);
            // 
            // PlayComputerButton
            // 
            this.PlayComputerButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PlayComputerButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.PlayComputerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PlayComputerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PlayComputerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayComputerButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PlayComputerButton.Location = new System.Drawing.Point(54, 128);
            this.PlayComputerButton.Name = "PlayComputerButton";
            this.PlayComputerButton.Size = new System.Drawing.Size(202, 81);
            this.PlayComputerButton.TabIndex = 1;
            this.PlayComputerButton.Text = "Play against the computer";
            this.PlayComputerButton.UseVisualStyleBackColor = false;
            this.PlayComputerButton.Click += new System.EventHandler(this.PlayComputerButton_Click);
            // 
            // PlayTwoPlayersButton
            // 
            this.PlayTwoPlayersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayTwoPlayersButton.Location = new System.Drawing.Point(272, 128);
            this.PlayTwoPlayersButton.Name = "PlayTwoPlayersButton";
            this.PlayTwoPlayersButton.Size = new System.Drawing.Size(202, 81);
            this.PlayTwoPlayersButton.TabIndex = 2;
            this.PlayTwoPlayersButton.Text = "Play against your friend";
            this.PlayTwoPlayersButton.UseVisualStyleBackColor = true;
            this.PlayTwoPlayersButton.Click += new System.EventHandler(this.PlayTwoPlayersButton_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(537, 255);
            this.Controls.Add(this.PlayTwoPlayersButton);
            this.Controls.Add(this.PlayComputerButton);
            this.Controls.Add(this.BoardSizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button PlayComputerButton;
        private System.Windows.Forms.Button PlayTwoPlayersButton;
    }
}