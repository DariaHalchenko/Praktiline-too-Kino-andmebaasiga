using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class Registreerimine : Form
    {
        Button btn1, btn2;
        Label lbl1, lbl2, lbl3;
        public Registreerimine()
        {
            this.Height = 500; 
            this.Width = 600;
            this.Text = "Registreerimine";
            // Устанавливаем картинку в качестве фона
            this.BackgroundImage = Image.FromFile(@"../../Registr.jpg");
            // Устанавливаем стиль растяжения изображения
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //btn1 - Kava
            btn1 = new Button();
            btn1.Text = "Administraator";
            btn1.Size = new Size(230, 52);
            btn1.Location = new Point(26,339);
            btn1.Font = new Font("Algerian", 18, FontStyle.Italic);
            Controls.Add(btn1);

            //btn2 - Osta pilet
            btn2 = new Button();
            btn2.Text = "Logi sisse";
            btn2.Size = new Size(171, 52);
            btn2.Location = new Point(300, 339);
            btn2.Font = new Font("Algerian", 18, FontStyle.Italic);
            Controls.Add(btn2);

            lbl1 = new Label();
            lbl1.AutoSize = true;
            lbl1.Text = "Nimi";
            lbl1.Font = new Font("Arial", 25, FontStyle.Italic);
            lbl1.TextAlign = ContentAlignment.TopLeft;
            lbl1.Location = new Point(97, 78);
            lbl1.Size = new Size(70, 30);
            Controls.Add(lbl1);

            lbl2 = new Label();
            lbl3 = new Label();
        }
    }
}
