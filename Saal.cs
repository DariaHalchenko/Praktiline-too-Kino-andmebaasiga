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
    public partial class Saal : Form
    {
        public event Action OnLaduAdded;  // Üritus vormi1 teavitamiseks

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\opilane\source\repos\Daria Halchenko TARpv23\Praktiline-too-Kino-andmebaasiga\Kino.mdf"";Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        int ID = 0;
        public Saal()
        {
            InitializeComponent();
        }
    }
}
