using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsServer
{
    public partial class AppendNews : Form
    {
        Image p1 = Image.FromHbitmap(NewsServer.Properties.Resources.closeiconStandard.GetHbitmap());
        Image p2 = Image.FromHbitmap(NewsServer.Properties.Resources.closeiconActive.GetHbitmap());

        public AppendNews()
        {
            InitializeComponent();
        }

        private void AppendNews_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(44, 46, 48);
            pictureBox1.BackgroundImage = p1;

            button1.BackColor = Color.FromArgb(170, 113, 232);
            button1.FlatAppearance.BorderSize = 0;

            richTextBox1.BackColor = Color.FromArgb(44, 46, 48);
            richTextBox2.BackColor = Color.FromArgb(44, 46, 48);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = p1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text = "SHOULD BE NOT EMPTY!";
                return;
            }
            if (richTextBox2.Text == "")
            {
                richTextBox2.Text = "SHOULD BE NOT EMPTY!";
                return;
            }

            string message = DateTime.Now.ToString("HH:mm") + "#" + richTextBox1.Text + "#" + richTextBox2.Text;

            Uploader.AppendNews(message);

            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = p2;
        }
    }
}
