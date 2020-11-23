using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrivialprojecteSO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int i;
            InitializeComponent();
            this.BackgroundImage = TrivialprojecteSO.Properties.Resources.fons2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            //WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 i = new Form2();
            i.Show();
            this.Hide();
        }
    }
}
