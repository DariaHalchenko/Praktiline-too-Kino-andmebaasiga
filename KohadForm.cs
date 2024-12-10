using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class KohadForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\opilane\source\repos\Daria Halchenko TARpv23\Praktiline-too-Kino-andmebaasiga\Kino.mdf"";Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable seansidTable;
        int ID;

        Label seansid_lbl, broneeringu_lbl, rida_lbl, koht_lbl;
        ComboBox seansid_cb;
        TextBox broneeringu_txt, rida_txt, koht_txt;
        Button lisa_btn, uuenda_btn, kustuta_btn;
        DataGridView dataGridView;

        public KohadForm()
        {
            this.Text = "Kohad";
            this.Size = new Size(800, 600);

            seansid_lbl = new Label();
            seansid_lbl.Text = "Seansi ID";
            seansid_lbl.Location = new Point(20, 20);
            seansid_lbl.AutoSize = true;
            Controls.Add(seansid_lbl);

            seansid_cb = new ComboBox();
            seansid_cb.Location = new Point(120, 20);
            seansid_cb.Width = 200;
            Controls.Add(seansid_cb);

            broneeringu_lbl = new Label();
            broneeringu_lbl.Text = "Broneeringu staatus";
            broneeringu_lbl.Location = new Point(20, 60);
            broneeringu_lbl.AutoSize = true;
            Controls.Add(broneeringu_lbl);

            broneeringu_txt = new TextBox();
            broneeringu_txt.Location = new Point(200, 60);
            broneeringu_txt.Width = 200;
            Controls.Add(broneeringu_txt);

            rida_lbl = new Label();
            rida_lbl.Text = "Rida number";
            rida_lbl.Location = new Point(20, 100);
            rida_lbl.AutoSize = true;
            Controls.Add(rida_lbl);

            rida_txt = new TextBox();
            rida_txt.Location = new Point(200, 100);
            rida_txt.Width = 200;
            Controls.Add(rida_txt);

            koht_lbl = new Label();
            koht_lbl.Text = "Koha number";
            koht_lbl.Location = new Point(20, 140);
            koht_lbl.AutoSize = true;
            Controls.Add(koht_lbl);

            koht_txt = new TextBox();
            koht_txt.Location = new Point(200, 140);
            koht_txt.Width = 200;
            Controls.Add(koht_txt);

            lisa_btn = new Button();
            lisa_btn.Text = "Lisa";
            lisa_btn.Location = new Point(20, 180);
            lisa_btn.Width = 100;
            lisa_btn.Click += Lisa_btn_Click;
            Controls.Add(lisa_btn);

            uuenda_btn = new Button();
            uuenda_btn.Text = "Uuenda";
            uuenda_btn.Location = new Point(130, 180);
            uuenda_btn.Width = 100;
            uuenda_btn.Click += Uuenda_btn_Click;
            Controls.Add(uuenda_btn);

            kustuta_btn = new Button();
            kustuta_btn.Text = "Kustuta";
            kustuta_btn.Location = new Point(240, 180);
            kustuta_btn.Width = 100;
            kustuta_btn.Click += Kustuta_btn_Click;
            Controls.Add(kustuta_btn);

            dataGridView = new DataGridView();
            dataGridView.Location = new Point(20, 240);
            dataGridView.Width = 740;
            dataGridView.Height = 300;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            Controls.Add(dataGridView);

            NaitaSeansid();
            NaitaKohad();
        }

        private void NaitaSeansid()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT Id, Start_time FROM Seansid", conn);
            adapter = new SqlDataAdapter(cmd);
            seansidTable = new DataTable();
            adapter.Fill(seansidTable);

            foreach (DataRow row in seansidTable.Rows)
            {
                seansid_cb.Items.Add(row["Start_time"]);
            }

            conn.Close();
        }

        private void NaitaKohad()
        {
            conn.Open();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT * FROM Kohad", conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView.DataSource = dt;
            conn.Close();
        }

        private void Lisa_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(seansid_cb.Text) && !string.IsNullOrEmpty(broneeringu_txt.Text) &&
                !string.IsNullOrEmpty(rida_txt.Text) && !string.IsNullOrEmpty(koht_txt.Text))
            {
                conn.Open();
                cmd = new SqlCommand("INSERT INTO Kohad (Seansid_Id, Broneeringu_staatus, Rida_number, Kohanumber) VALUES (@seansid, @broneeringu, @rida, @koht)", conn);
                cmd.Parameters.AddWithValue("@seansid", seansid_cb.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@broneeringu", broneeringu_txt.Text);
                cmd.Parameters.AddWithValue("@rida", rida_txt.Text);
                cmd.Parameters.AddWithValue("@koht", koht_txt.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                NaitaKohad();
            }
            else
            {
                MessageBox.Show("Palun täitke kõik väljad.");
            }
        }

        private void Uuenda_btn_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                conn.Open();
                cmd = new SqlCommand("UPDATE Kohad SET Seansid_Id=@seansid, Broneeringu_staatus=@broneeringu, Rida_number=@rida, Kohanumber=@koht WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@seansid", seansid_cb.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@broneeringu", broneeringu_txt.Text);
                cmd.Parameters.AddWithValue("@rida", rida_txt.Text);
                cmd.Parameters.AddWithValue("@koht", koht_txt.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                NaitaKohad();
            }
            else
            {
                MessageBox.Show("Valige rida uuendamiseks.");
            }
        }

        private void Kustuta_btn_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                conn.Open();
                cmd = new SqlCommand("DELETE FROM Kohad WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                conn.Close();
                NaitaKohad();
            }
            else
            {
                MessageBox.Show("Valige rida kustutamiseks.");
            }
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["Id"].Value);
            seansid_cb.Text = dataGridView.Rows[e.RowIndex].Cells["Seansid_Id"].Value.ToString();
            broneeringu_txt.Text = dataGridView.Rows[e.RowIndex].Cells["Broneeringu_staatus"].Value.ToString();
            rida_txt.Text = dataGridView.Rows[e.RowIndex].Cells["Rida_number"].Value.ToString();
            koht_txt.Text = dataGridView.Rows[e.RowIndex].Cells["Kohanumber"].Value.ToString();
        }
    }
}
