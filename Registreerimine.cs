using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class Registreerimine : Form
    {
        Button btn1;
        Label lbl1, lbl2, lbl3, lbl4;
        TextBox txt1, txt2, txt3;
        ComboBox rolli_cb;
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\opilane\source\repos\Daria Halchenko TARpv23\Praktiline-too-Kino-andmebaasiga\Kino.mdf"";Integrated Security=True");
        SqlCommand cmd;

        public Registreerimine()
        {
            this.Height = 500; 
            this.Width = 600;
            this.Text = "Registreerimine";
            // Устанавливаем картинку в качестве фона
            this.BackgroundImage = Image.FromFile(@"../../Registr.jpg");
            // Устанавливаем стиль растяжения изображения
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //btn1 - Logi sisse
            btn1 = new Button();
            btn1.Text = "Logi sisse";
            btn1.Size = new Size(171, 52);
            btn1.Location = new Point(200, 339);
            btn1.Font = new Font("Algerian", 18, FontStyle.Italic);
            Controls.Add(btn1);

            //Nimi
            lbl1 = new Label();
            lbl1.AutoSize = true;
            lbl1.Text = "Nimi";
            lbl1.Font = new Font("Arial", 25, FontStyle.Italic);
            lbl1.TextAlign = ContentAlignment.TopLeft;
            lbl1.Location = new Point(97, 78);
            lbl1.Size = new Size(70, 30);
            Controls.Add(lbl1);

            txt1 = new TextBox();
            txt1.Location = new Point(220, 78);
            txt1.Font = new Font("Arial", 15);
            txt1.Width = 180;
            Controls.Add(txt1);

            //E-mail
            lbl2 = new Label();
            lbl2.AutoSize = true;
            lbl2.Text = "E-mail";
            lbl2.Font = new Font("Arial", 25, FontStyle.Italic);
            lbl2.TextAlign = ContentAlignment.TopLeft;
            lbl2.Location = new Point(97, 140);
            lbl2.Size = new Size(70, 30);
            Controls.Add(lbl2);

            txt2 = new TextBox();
            txt2.Location = new Point(220, 140);
            txt2.Font = new Font("Arial", 15);
            txt2.Width = 180;
            Controls.Add(txt2);

            //Parool
            lbl3 = new Label();
            lbl3.AutoSize = true;
            lbl3.Text = "Parool";
            lbl3.Font = new Font("Arial", 25, FontStyle.Italic);
            lbl3.TextAlign = ContentAlignment.TopLeft;
            lbl3.Location = new Point(97, 200);
            lbl3.Size = new Size(70, 30);
            Controls.Add(lbl3);

            txt3 = new TextBox();
            txt3.Location = new Point(220, 200);
            txt3.Font = new Font("Arial", 15);
            txt3.Width = 180;
            Controls.Add(txt3);

            //Rolli
            lbl4 = new Label();
            lbl4.AutoSize = true;
            lbl4.Text = "Rolli";
            lbl4.Font = new Font("Arial", 25, FontStyle.Italic);
            lbl4.TextAlign = ContentAlignment.TopLeft;
            lbl4.Location = new Point(97, 250);
            lbl4.Size = new Size(70, 30);
            Controls.Add(lbl4);

            rolli_cb = new ComboBox();
            rolli_cb.Location = new Point(220, 250);
            rolli_cb.Font = new Font("Arial", 15);
            rolli_cb.Width = 180;
            Controls.Add(rolli_cb);

            rolli_cb.Items.Add("Admin");
            rolli_cb.Items.Add("Klient");

            NaitaAndmed();

        }
        public void NaitaAndmed()
        {
            string nimi = txt1.Text;
            string email = txt2.Text;
            string parool = txt3.Text;
            string rolli = rolli_cb.SelectedItem.ToString();

            conn.Open();
            cmd = new SqlCommand("INSERT INTO  Registreerimini(Nimi, E-mail, Parool, Rolli) VALUES (@nimi, @email, @parool, @rolli)", conn);
            cmd.Parameters.AddWithValue("@nimi", nimi);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@parool", parool);
            cmd.Parameters.AddWithValue("@rolli", rolli);
            cmd.ExecuteNonQuery();
            conn.Close();

            // Теперь переходим к соответствующей форме в зависимости от роли
            if (rolli == "Admin")
            {
                // Открываем форму Кассы для "müüja"
                Table table = new Table();
                table.Show();
            }
            else if (rolli == "Klient")
            {
                // Открываем основную форму для "omanik" (Form1)
                Kino kino = new Kino();
                kino.Show();
            }
        }
    }
}
