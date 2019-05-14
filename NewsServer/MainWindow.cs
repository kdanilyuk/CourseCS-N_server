using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsServer
{
    public partial class MainWindow : Form
    {
        Image p1 = Image.FromHbitmap(NewsServer.Properties.Resources.closeiconStandard.GetHbitmap());
        Image p2 = Image.FromHbitmap(NewsServer.Properties.Resources.closeiconActive.GetHbitmap());

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(44, 46, 48);

            statusLabel.ForeColor = Color.FromArgb(237, 64, 64);
            contentBox.ForeColor = Color.White;
            contentBox.BackColor = Color.FromArgb(44, 46, 48);
            startButton.BackColor = Color.FromArgb(170, 113, 232);
            startButton.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(170, 113, 232);
            button1.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.FromArgb(170, 113, 232);
            button2.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.FromArgb(170, 113, 232);
            button3.FlatAppearance.BorderSize = 0;

            closer.BackgroundImage = p1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "ON";
            statusLabel.ForeColor = Color.FromArgb(96, 255, 33);
            Thread serverThread = new Thread(new ThreadStart(ServerObject.StartServer));
            serverThread.Start();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            closer.BackgroundImage = p2;
        }

        private void closer_MouseLeave(object sender, EventArgs e)
        {
            closer.BackgroundImage = p1;
        }

        private void closer_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            contentBox.Text = "";
            string[] login = Uploader.InitLogin();
            string[] password = Uploader.InitPassword();
            for (int i = 0; i < login.Length; i++)
            {
                contentBox.AppendText((i+1).ToString() + ":\tLOGIN:" + login[i] + "\tPASSWORD:" + password[i] + "\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contentBox.Text = "";

            string actNews = Uploader.GetActualNews();
            string[] words = actNews.Split(new char[] { '#' });
            for (int i = 0; i < words.Length - 1; i++)
            {
                words[i] = words[i].Replace("\n", "");
            }

            for (int i = words.Length - 2; i > 0; i--)
            {
                contentBox.AppendText(words[i - 2] + " " + words[i - 1] + "\n");
                contentBox.AppendText("\t\t" + words[i] + "\n\n");
                i -= 2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppendNews ap = new AppendNews();
            ap.Show();
        }
    }
}
