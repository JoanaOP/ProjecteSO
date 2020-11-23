using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrivialprojecteSO
{
    class Class1
    {
        private int numero;
        public void Llançar(Random r)
        {
            numero = r.Next(6) + 1;
        }
        public int ObtenirNumero()
        {
            return numero;
        }
        public void MostrarImatge(PictureBox pb)
        {
            if (numero == 1)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.blau;
            }
            if (numero == 2)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.groc;
            }
            if (numero == 3)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.lila;
            }
            if (numero == 4)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.taronja;
            }
            if (numero == 5)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.verd;
            }
            if (numero == 1)
            {
                pb.Image = TrivialprojecteSO.Properties.Resources.vermell;
            }

        }
    }

}
