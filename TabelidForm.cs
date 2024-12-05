using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class TabelidForm : Form
    {
        Button Kinolaud_btn, Saal_btn;
        public TabelidForm()
        {
            this.Height = 500;
            this.Width = 700;
            this.Text = "Tabelid";

            // Button - Kinolaud_btn
            Kinolaud_btn = new Button();
            Kinolaud_btn.Font = new Font("Arial", 20, FontStyle.Bold);
            Kinolaud_btn.Location = new Point(35, 180);
            Kinolaud_btn.Size = new Size(110, 40);
            Kinolaud_btn.Text = "Kinolaud";
            Controls.Add(Kinolaud_btn);
            Kinolaud_btn.Click += Kinolaud_btn_Click; 

            // Button - uuenda_btn
            Saal_btn = new Button();
            Saal_btn.Font = new Font("Arial", 15, FontStyle.Bold);
            Saal_btn.Location = new Point(267, 180);
            Saal_btn.Size = new Size(110, 40);
            Saal_btn.Text = "Lauasaal";
            Controls.Add(Saal_btn);
            Saal_btn.Click += Saal_btn_Click; 
        }

        private void Saal_btn_Click(object sender, EventArgs e)
        {
            LauasaalForm lauasaal = new LauasaalForm();
            lauasaal.Show();
        }

        private void Kinolaud_btn_Click(object sender, EventArgs e)
        {
            KinolaudForm kinolaud = new KinolaudForm();
            kinolaud.Show();
        }
    }
}
