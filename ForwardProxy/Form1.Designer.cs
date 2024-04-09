namespace ForwardProxy
{
    partial class Form1
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
            tableLayoutPanel1 = new TableLayoutPanel();
            logTextBox = new ListBox();
            cacheItems = new ListBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            viewLogButton = new Button();
            viewCacheButton = new Button();
            viewItemButton = new Button();
            clearCacheButton = new Button();
            imageBlockContents = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(logTextBox, 0, 0);
            tableLayoutPanel1.Controls.Add(cacheItems, 1, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(imageBlockContents, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1148, 501);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // logTextBox
            // 
            logTextBox.Dock = DockStyle.Fill;
            logTextBox.FormattingEnabled = true;
            logTextBox.ItemHeight = 15;
            logTextBox.Location = new Point(3, 3);
            logTextBox.Name = "logTextBox";
            logTextBox.Size = new Size(568, 244);
            logTextBox.TabIndex = 0;
            // 
            // cacheItems
            // 
            cacheItems.Dock = DockStyle.Fill;
            cacheItems.FormattingEnabled = true;
            cacheItems.ItemHeight = 15;
            cacheItems.Location = new Point(577, 3);
            cacheItems.Name = "cacheItems";
            cacheItems.Size = new Size(568, 244);
            cacheItems.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(viewLogButton);
            flowLayoutPanel1.Controls.Add(viewCacheButton);
            flowLayoutPanel1.Controls.Add(viewItemButton);
            flowLayoutPanel1.Controls.Add(clearCacheButton);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 253);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(568, 245);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // viewLogButton
            // 
            viewLogButton.Location = new Point(3, 3);
            viewLogButton.Name = "viewLogButton";
            viewLogButton.Size = new Size(75, 23);
            viewLogButton.TabIndex = 0;
            viewLogButton.Text = "View Log";
            viewLogButton.UseVisualStyleBackColor = true;
            viewLogButton.Click += viewLogButton_Click;
            // 
            // viewCacheButton
            // 
            viewCacheButton.Location = new Point(84, 3);
            viewCacheButton.Name = "viewCacheButton";
            viewCacheButton.Size = new Size(136, 23);
            viewCacheButton.TabIndex = 1;
            viewCacheButton.Text = "View Cache Items";
            viewCacheButton.UseVisualStyleBackColor = true;
            viewCacheButton.Click += viewCacheButton_Click;
            // 
            // viewItemButton
            // 
            viewItemButton.Location = new Point(226, 3);
            viewItemButton.Name = "viewItemButton";
            viewItemButton.Size = new Size(128, 23);
            viewItemButton.TabIndex = 2;
            viewItemButton.Text = "View Selected Item";
            viewItemButton.UseVisualStyleBackColor = true;
            viewItemButton.Click += viewItemButton_Click;
            // 
            // clearCacheButton
            // 
            clearCacheButton.Location = new Point(360, 3);
            clearCacheButton.Name = "clearCacheButton";
            clearCacheButton.Size = new Size(122, 23);
            clearCacheButton.TabIndex = 3;
            clearCacheButton.Text = "Clear Cache";
            clearCacheButton.UseVisualStyleBackColor = true;
            clearCacheButton.Click += clearCacheButton_Click;
            // 
            // imageBlockContents
            // 
            imageBlockContents.Dock = DockStyle.Fill;
            imageBlockContents.Location = new Point(577, 253);
            imageBlockContents.Multiline = true;
            imageBlockContents.Name = "imageBlockContents";
            imageBlockContents.ReadOnly = true;
            imageBlockContents.Size = new Size(568, 245);
            imageBlockContents.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1148, 501);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ListBox logTextBox;
        private ListBox cacheItems;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button viewLogButton;
        private Button viewCacheButton;
        private Button viewItemButton;
        private Button clearCacheButton;
        private TextBox imageBlockContents;
    }
}