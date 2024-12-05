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
    public partial class KinoForm : Form
    {
        Button btn1, btn2;
        public KinoForm()
        {
            this.Height = 676; 
            this.Width = 881;
            this.Text = "Tere tulemast kinno!";
            // Устанавливаем картинку в качестве фона
            this.BackgroundImage = Image.FromFile(@"../../Taustal.jpg");
            // Устанавливаем стиль растяжения изображения
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //btn1 - Kava
            btn1 = new Button();
            btn1.Text = "Kava";
            btn1.Size = new Size(152, 55);
            btn1.Location = new Point(58, 84);
            btn1.Font= new Font("Algerian", 18, FontStyle.Italic);
            Controls.Add(btn1);

            //btn2 - Osta pilet
            btn2 = new Button();
            btn2.Text = "Osta pilet";
            btn2.Size = new Size(152,55);
            btn2.Location = new Point(58, 214);
            btn2.Font = new Font("Algerian", 18, FontStyle.Italic);
            Controls.Add(btn2);
        }
    }
}
