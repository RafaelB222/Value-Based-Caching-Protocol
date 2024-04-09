namespace ClientGUI
{
    partial class ClientGUI
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
            getSelectedImage = new Button();
            pictureBox1 = new PictureBox();
            listBox1 = new ListBox();
            getImageList = new Button();
            viewCacheButton = new Button();
            openServerGUI = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // getSelectedImage
            // 
            getSelectedImage.Location = new Point(661, 53);
            getSelectedImage.Name = "getSelectedImage";
            getSelectedImage.Size = new Size(127, 23);
            getSelectedImage.TabIndex = 0;
            getSelectedImage.Text = "Get Selected Image";
            getSelectedImage.UseMnemonic = false;
            getSelectedImage.UseVisualStyleBackColor = true;
            getSelectedImage.Click += getSelectedImage_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(353, 301);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(439, 53);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(216, 199);
            listBox1.TabIndex = 2;
            // 
            // getImageList
            // 
            getImageList.Location = new Point(439, 296);
            getImageList.Name = "getImageList";
            getImageList.Size = new Size(154, 23);
            getImageList.TabIndex = 3;
            getImageList.Text = "Get Image List";
            getImageList.UseVisualStyleBackColor = true;
            getImageList.Click += getImageList_Click;
            // 
            // viewCacheButton
            // 
            viewCacheButton.Location = new Point(615, 296);
            viewCacheButton.Name = "viewCacheButton";
            viewCacheButton.Size = new Size(154, 23);
            viewCacheButton.TabIndex = 4;
            viewCacheButton.Text = "View Cache Contents";
            viewCacheButton.UseVisualStyleBackColor = true;
            viewCacheButton.Click += viewCacheButton_Click;
            // 
            // openServerGUI
            // 
            openServerGUI.Location = new Point(439, 354);
            openServerGUI.Name = "openServerGUI";
            openServerGUI.Size = new Size(129, 23);
            openServerGUI.TabIndex = 5;
            openServerGUI.Text = "Open Server GUI";
            openServerGUI.UseVisualStyleBackColor = true;
            openServerGUI.Click += openServerGUI_Click;
            // 
            // ClientGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(openServerGUI);
            Controls.Add(viewCacheButton);
            Controls.Add(getImageList);
            Controls.Add(listBox1);
            Controls.Add(pictureBox1);
            Controls.Add(getSelectedImage);
            Name = "ClientGUI";
            Text = "ClientGUI";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button getSelectedImage;
        private PictureBox pictureBox1;
        private ListBox listBox1;
        private Button getImageList;
        private Button viewCacheButton;
        private Button openServerGUI;
    }
}