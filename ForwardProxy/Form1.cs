using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace ForwardProxy
{
    internal partial class Form1 : Form
    {
        private Cache _cache;
        private ProxyServerLog _log;
        private NetworkStream _stream;

        public Form1(Cache cache, ProxyServerLog log, NetworkStream serverStream)
        {
            _cache = cache;
            _log = log;
            _stream = serverStream;
            InitializeComponent();

        }

        private void viewLogButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("viewing log");
            foreach (LogEntry entry in _log.GetLog())
            {
                logTextBox.Items.Add(entry.GetRequestMessage());
                logTextBox.Items.Add(entry.GetResponseMessage());

            }
        }

        private void viewCacheButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("viewing cache items");
            foreach (string key in _cache.Keys)
            {
                cacheItems.Items.Add(key);
            }
        }

        private void viewItemButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("viewing item");
            ImageBlock selectedBlock = _cache.GetItemFromCache(cacheItems.SelectedItem.ToString());
            StringBuilder hexString = new StringBuilder(selectedBlock.ImageData.Length * 2);

            foreach (byte b in selectedBlock.ImageData)
            {
                hexString.AppendFormat("{0:X2}", b);
            }

            string hexRepresentation = hexString.ToString();
            imageBlockContents.Text = hexRepresentation;
        }

        private async void clearCacheButton_Click(object sender, EventArgs e)
        {
            TcpHelper tcpHelper = new TcpHelper();
            byte[] messageBytes = Encoding.ASCII.GetBytes("clearSentBlocks");
            _cache.Clear();
            _log.Clear();
            logTextBox.Items.Clear();
            cacheItems.Items.Clear();
            imageBlockContents.Text= string.Empty;
            await tcpHelper.ForwardMessageToServer(4, messageBytes, _stream);
            MessageBox.Show("Cache Cleared");
        }
    }
}
