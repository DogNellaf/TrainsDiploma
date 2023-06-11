using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;
using TrainsClasses.Enums;
using System.Diagnostics;
using Xceed.Words.NET;
using System.IO;
using Xceed.Document.NET;

namespace ui.AdminWorkspacePages
{

    public partial class Reports : Window
    {
        private Window _previous;
        private User _worker;
        private string path = "report.docx";
        private Formatting titleFormat;
        private Formatting textFormat;

        public Reports(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _worker = admin;

            //Formatting Title  
            titleFormat = new Formatting();

            //Specify font family  
            titleFormat.FontFamily = new Font("Times New Roman");

            //Specify font size  
            titleFormat.Size = 18d;
            titleFormat.Position = 40;
            titleFormat.Bold = true;
            titleFormat.Spacing = 0;
            //titleFormat.Spacing = 1.5;

            //Formatting Text Paragraph  
            textFormat = new Formatting();
            //font family  
            textFormat.FontFamily = new Font("Times New Roman");
            //font size  
            textFormat.Size = 10d;
            //Spaces between characters  
            //textFormat.Spacing = 1.5;
        }

        private DocX CreateReport()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return DocX.Create(path);
        }
        
        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            var doc = CreateReport();

            var paragraph = doc.InsertParagraph(ticketsButton.Content.ToString(), false, titleFormat);
            paragraph.LineSpacing = 15;

            var tickets = RequestClient.GetObjects<Ticket>().Where(x => x.BuyTime.Month == DateTime.Now.Month && x.StatusId != 2).ToList();
            var routes = RequestClient.GetObjects<Route>();
            var cities = RequestClient.GetObjects<City>();

            Table t = doc.AddTable(tickets.Count + 1, 3);
            t.Alignment = Alignment.center;

            t.Rows[0].Cells[0].Paragraphs.First().Append("Время оформления");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Цена");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Направление");

            float sum = 0;

            for (int i = 0; i < tickets.Count; i++)
            {
                var ticket = tickets[i];
                sum += ticket.Price;

                var route = routes.Where(x => x.Id == ticket.RouteId).First();
                var cityTo = cities.Find(x => x.Id == route.ArrivalCityId).Name.Trim();
                var cityFrom = cities.Find(x => x.Id == route.DepartureCityId).Name.Trim();

                t.Rows[i + 1].Cells[0].Paragraphs.First().Append($"{ticket.BuyTime}");
                t.Rows[i + 1].Cells[1].Paragraphs.First().Append($"{ticket.Price}");
                t.Rows[i + 1].Cells[2].Paragraphs.First().Append($"Рейс {route.Id} {cityFrom} - {cityTo}");
            }

            doc.InsertTable(t);

            doc.InsertParagraph($"Всего куплено билетов: {tickets.Count}", false, textFormat);

            doc.InsertParagraph($"Куплено билетов на сумму: {sum}", false, textFormat);

            doc.Save();

            MessageBox.Show($"Отчет сохранен в файл {path}");
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            var doc = CreateReport();

            var login = Microsoft.VisualBasic.Interaction.InputBox("Введите login:");

            var user = RequestClient.GetObjects<User>().Find(x => x.Login.Trim() == login);

            if (user is null)
            {
                MessageBox.Show("Такой пользователь не существует");
                return;
            }

            var paragraph = doc.InsertParagraph($"Транзакции пользователя {login}", false, titleFormat);
            paragraph.LineSpacing = 15;

            var transactions = RequestClient.GetObjects<Transaction>().Where(x => x.UserId == user.Id).ToList();

            Table t = doc.AddTable(transactions.Count + 1, 5);
            t.Alignment = Alignment.center;

            t.Rows[0].Cells[0].Paragraphs.First().Append("Изменение");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Завершена ли");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Время");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Тип оплаты");
            t.Rows[0].Cells[4].Paragraphs.First().Append("Комментарий");

            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];

                string isComplited = "Да";
                if (!transaction.IsComplited)
                {
                    isComplited = "Нет";
                }

                t.Rows[i + 1].Cells[0].Paragraphs.First().Append($"{transaction.Value}");
                t.Rows[i + 1].Cells[1].Paragraphs.First().Append(isComplited);
                t.Rows[i + 1].Cells[2].Paragraphs.First().Append($"{transaction.PaymentTime}");
                t.Rows[i + 1].Cells[3].Paragraphs.First().Append($"{transaction.PaymentType}");
                t.Rows[i + 1].Cells[4].Paragraphs.First().Append($"{transaction.Comment}");
            }

            doc.InsertTable(t);

            doc.Save();

            MessageBox.Show($"Отчет сохранен в файл {path}");
        }

        private void routeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
