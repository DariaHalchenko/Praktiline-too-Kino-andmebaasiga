using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class PiletiOstmiseForm : Form
    {
        // Класс, представляющий кинотеатр (зал)
        public class KinoSaal
        {
            public int RidadeArv { get; set; }
            public int KohtadeArv { get; set; }

            public KinoSaal(int ridadeArv, int kohtadeArv)
            {
                RidadeArv = ridadeArv;
                KohtadeArv = kohtadeArv;
            }
        }

        private Random random; // Генератор случайных чисел
        private List<Button> buttons; // Список кнопок для мест
        private KinoSaal kinosaal; // Один случайный зал
        Button btn;
        public PiletiOstmiseForm()
        {
            InitializeComponent();
            buttons = new List<Button>();
            random = new Random();

            Load += (s, e) =>
            {
                // Создаем один случайный зал с случайными размерами
                kinosaal = RandomSaal();

                // Устанавливаем размер формы в зависимости от размеров зала
                VormiSuurus();

                // Отображаем зал на форме
                SaaliKuvamine();
            };
        }

        // Метод для создания случайного зала
        private KinoSaal RandomSaal()
        {
            int rida = random.Next(5, 13); // Максимум 12 рядов
            int koht = random.Next(5, 15); // Максимум 14 мест
            return new KinoSaal(rida, koht);
        }

        // Метод для настройки размера формы в зависимости от количества рядов и мест
        private void VormiSuurus()
        {
            // Определим отступы между рядами и местами
            int buttonWidth = 60;  // Ширина кнопки
            int buttonHeight = 60; // Высота кнопки
            int horizontalPadding = 30; // Отступы по горизонтали
            int verticalPadding = 50;   // Отступы по вертикали

            // Устанавливаем ширину формы в зависимости от количества мест в ряду
            this.Width = (kinosaal.KohtadeArv * buttonWidth) + (kinosaal.KohtadeArv - 1) * horizontalPadding + 50;

            // Устанавливаем высоту формы в зависимости от количества рядов
            this.Height = (kinosaal.RidadeArv * buttonHeight) + (kinosaal.RidadeArv - 1) * verticalPadding + 100;
        }

        // Метод для отображения одного зала на форме
        private void SaaliKuvamine()
        {
            int Y = 10;  // Смещение по вертикали для отображения мест
            int X = 10;  // Смещение по горизонтали для отображения мест

            // Создаем кнопки для мест в зале
            for (int i = 0; i < kinosaal.RidadeArv; i++)
            {
                for (int j = 0; j < kinosaal.KohtadeArv; j++)
                {
                    btn = new Button();
                    btn.Size = new Size(60, 60);
                    btn.Location = new Point(X + j * 90, Y + i * 70);
                    btn.Text = $"{i + 1}-{j + 1}";
                    btn.Font = new Font("Arial", 10, FontStyle.Bold);
                    btn.Name = $"Btn-{i + 1}-{j + 1}";
                    btn.BackColor = Color.Green;
                    btn.Tag = "available";
                    btn.Click += Btn_Click;
                    buttons.Add(btn);
                    Controls.Add(btn);
                }
            }

            // Генерируем случайные забронированные места для зала
            KohtadeBroneeringud();
        }

        // Обработчик клика по кнопке
        private void Btn_Click(object sender, EventArgs e)
        {
            if (btn.Tag.ToString() == "available")
            {
                btn.BackColor = Color.Red;  // Забронировать место (красное)
                btn.Tag = "booked";  // Изменяем статус места
            }
            else
            {
                btn.BackColor = Color.Green;  // Освободить место (зеленое)
                btn.Tag = "available";  // Изменяем статус места
            }
        }

        // Метод для случайного бронирования мест в зале
        private void KohtadeBroneeringud()
        {
            int ReserveeritudKohtadaArv = random.Next(5, kinosaal.RidadeArv * kinosaal.KohtadeArv / 2);  // Случайное количество забронированных мест

            for (int i = 0; i < ReserveeritudKohtadaArv; i++)
            {
                // Генерируем случайные индексы для ряда и места
                int rida1 = random.Next(0, kinosaal.RidadeArv);
                int kohad = random.Next(0, kinosaal.KohtadeArv);

                Button btn = buttons.FirstOrDefault(b => b.Name == $"Btn-{rida1 + 1}-{kohad + 1}");

                // Если место ещё не забронировано, бронируем его
                if (btn != null && btn.Tag.ToString() == "available")
                {
                    btn.BackColor = Color.Red;  // Забронировать место (красное)
                    btn.Tag = "booked";  // Изменяем статус места
                }
                else
                {
                    i--;  // Если место уже забронировано, пробуем снова
                }
            }
        }
    }
}
