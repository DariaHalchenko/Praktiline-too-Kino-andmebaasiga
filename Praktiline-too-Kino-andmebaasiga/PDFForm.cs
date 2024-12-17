using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Praktiline_too_Kino_andmebaasiga
{
    public partial class PDFForm : Form
    {
        private string selectedPosterPath; // Путь к изображению выбранного фильма
        private string selectedMovieTitle; // Название фильма
        private string selectedSeats; // Выбранные места
        private string randomHall; // Случайный зал

        public PDFForm(string posterPath, string movieTitle, string seats)
        {
            InitializeComponent();
            selectedPosterPath = posterPath;
            selectedMovieTitle = movieTitle;
            selectedSeats = seats;
            randomHall = $"Hall {new Random().Next(1, 9)}"; // Генерация случайного зала

            // Интерфейс формы
            Label emailLabel = new Label()
            {
                Text = "Enter your email:",
                Location = new Point(20, 20),
                Size = new Size(200, 30)
            };
            TextBox emailTextBox = new TextBox()
            {
                Location = new Point(20, 60),
                Size = new Size(300, 30)
            };
            Button saveAndSendBtn = new Button()
            {
                Text = "Save PDF and Send",
                Location = new Point(20, 100),
                Size = new Size(200, 30)
            };
            saveAndSendBtn.Click += (sender, e) => SaveAndSendPDF(emailTextBox.Text);
            Controls.Add(emailLabel);
            Controls.Add(emailTextBox);
            Controls.Add(saveAndSendBtn);
        }

        private void SaveAndSendPDF(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter an email address.");
                return;
            }

            // Создание PDF-документа
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 18, XFontStyle.Bold);

            // Рисуем название фильма
            gfx.DrawString($"Movie: {selectedMovieTitle}", font, XBrushes.Black, new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);

            // Рисуем зал и места
            gfx.DrawString($"Hall: {randomHall}", font, XBrushes.Black, new XRect(50, 100, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Seats: {selectedSeats}", font, XBrushes.Black, new XRect(50, 150, page.Width, page.Height), XStringFormats.TopLeft);

            // Добавление изображения фильма
            if (File.Exists(selectedPosterPath))
            {
                XImage posterImage = XImage.FromFile(selectedPosterPath);
                gfx.DrawImage(posterImage, 50, 200, 200, 300); // Размещаем изображение
            }

            // Добавление штрих-кода (как текст для простоты)
            gfx.DrawString($"Barcode: {Guid.NewGuid()}", font, XBrushes.Black, new XRect(50, 550, page.Width, page.Height), XStringFormats.TopLeft);

            // Сохранение PDF в файл
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MovieTicket.pdf");
            document.Save(pdfPath);

            // Отправка PDF на почту
            SendEmailWithAttachment(email, pdfPath);
        }

        private void SendEmailWithAttachment(string email, string filePath)
        {
            try
            {
                string fromEmail = "your-email@gmail.com"; // Замените на ваш email
                string password = "your-email-password"; // Замените на пароль

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "Your Movie Ticket",
                    Body = "Attached is your movie ticket in PDF format.",
                    IsBodyHtml = true
                };

                mail.To.Add(email);
                mail.Attachments.Add(new Attachment(filePath));

                smtpClient.Send(mail);
                MessageBox.Show("Ticket sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}");
            }
        }
    }
}
