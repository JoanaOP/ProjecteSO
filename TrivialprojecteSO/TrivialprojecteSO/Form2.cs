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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.BackgroundImage = TrivialprojecteSO.Properties.Resources.fons_joc;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        Class1 dau = new Class1();
        Random r = new Random();

        private void boto_iniciar_Click(object sender, EventArgs e)
        {
            dau.Llançar(r);
            dau.MostrarImatge(fotoDau);
        }

    }
}
