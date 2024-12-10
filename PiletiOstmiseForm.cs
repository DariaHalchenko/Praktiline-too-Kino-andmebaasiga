using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class PiletiOstmiseForm : Form
    {
        // Подключение к базе данных
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\opilane\source\repos\Daria Halchenko TARpv23\Praktiline-too-Kino-andmebaasiga\Kino.mdf"";Integrated Security=True");
        SqlCommand cmd; // Команда для выполнения SQL-запросов
        SqlDataReader reader; // Читатель для считывания данных из базы
        FlowLayoutPanel flp; // Панель для размещения кнопок с местами
        List<Button> nimekirja_Buttons; // Список кнопок для мест

        public PiletiOstmiseForm()
        {
            // Устанавливаем размеры и название формы
            this.Height = 600;
            this.Width = 800;
            this.Text = "Pileti ostmine";

            // Создаем панель для размещения кнопок и настраиваем её
            flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Fill; // Панель заполняет всю форму
            flp.AutoScroll = true; // Включаем прокрутку, если места не помещаются
            Controls.Add(flp); // Добавляем панель на форму

            // Инициализируем список кнопок
            nimekirja_Buttons = new List<Button>();

            // Загружаем план зала
            SaaliPlaan();
        }

        // Метод для загрузки плана зала и создания кнопок для мест
        private void SaaliPlaan()
        {
            try
            {
                // Открываем соединение с базой данных
                conn.Open();
                // Запрос для получения количества рядов и мест в каждом ряду
                cmd = new SqlCommand("SELECT rida, kohad_reas FROM saal ORDER BY NEWID()", conn);
                reader = cmd.ExecuteReader(); // Выполняем запрос и читаем результаты

                int rida = 0;
                int kohad_rida = 0;

                // Если данные успешно получены, считываем их
                if (reader.Read())
                {
                    rida = Convert.ToInt32(reader["rida"]);
                    kohad_rida = Convert.ToInt32(reader["kohad_reas"]);
                }

                // Закрываем читателя после получения данных
                reader.Close();
                conn.Close();

                // Цикл для создания кнопок для каждого места
                for (int rid = 0; rid < rida; rid++)
                {
                    for (int seat = 0; seat < kohad_rida; seat++)
                    {
                        Button istmed_btn = new Button();
                        istmed_btn.Text = $"{rid + 1}-{seat + 1}"; // Текст кнопки указывает номер места
                        istmed_btn.Size = new Size(60, 60); // Устанавливаем размер кнопки
                        istmed_btn.Font = new Font("Arial", 10, FontStyle.Bold); // Настройка шрифта кнопки
                        istmed_btn.Click += SeatButton_Click; // Обработчик события клика

                        // Проверка, занято ли место
                        if (Hoivatud(rid + 1, seat + 1))
                        {
                            istmed_btn.BackColor = Color.Red; // Если занято, кнопка красного цвета
                            istmed_btn.Enabled = false; // Отключаем кнопку для выбора
                        }
                        else
                        {
                            istmed_btn.BackColor = Color.Green; // Если свободно, кнопка зеленого цвета
                        }

                        // Добавляем кнопку на панель
                        flp.Controls.Add(istmed_btn);
                    }
                    // Добавляем пустую метку между рядами для разделения
                    flp.Controls.Add(new Label { Text = "", Width = 10 });
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при загрузке плана зала
                MessageBox.Show("Ошибка при загрузке плана зала: " + ex.Message);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Метод для проверки занятости места, включая статус бронирования
        private bool Hoivatud(int rid, int iste)
        {
            bool hoivatud1 = false;
            try
            {
                conn.Open();
                // Запрос для проверки, занято ли место, включая статус бронирования
                cmd = new SqlCommand("SELECT Broneeringu_staatus FROM Kohad WHERE rida = @rida AND koht = @koht", conn);
                cmd.Parameters.AddWithValue("@rida", rid);
                cmd.Parameters.AddWithValue("@koht", iste);

                // Выполняем запрос и получаем значение статуса бронирования
                object tulemus = cmd.ExecuteScalar();

                if (tulemus != null && tulemus.ToString() == "broneeritud")
                {
                    hoivatud1 = true; // Если статус "broneeritud", место занято
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                // Обработка исключений при проверке занятости места
                MessageBox.Show("Ошибка при проверке занятости места: " + ex.Message);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return hoivatud1;
        }

        // Обработчик клика по кнопке с местом
        private void SeatButton_Click(object sender, EventArgs e)
        {
            //Button clickedButton = (Button)sender;
            //// Вывод сообщения о выбранном месте
            //MessageBox.Show($"Вы выбрали место: {clickedButton.Text}");
            //// Здесь можно добавить логику для бронирования выбранного места
        }
    }
}
