using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerGUI : Form
    {
        private string _destinationFolder;
        public ServerGUI(string destinationFolder)
        {
            InitializeComponent();
            _destinationFolder = destinationFolder;
        }

        private void addImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                string fileName = openFileDialog1.SafeFileName;
                string filePath = openFileDialog1.FileName;


                // Copy the file to the destination folder
                File.Copy(filePath, Path.Combine(_destinationFolder, fileName), true);
                MessageBox.Show("image saved to in folder: " + _destinationFolder);
            }
        }
    }
}
