using System;
using System.IO;
using System.Linq;
using System.Windows;
using TrainsClasses;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ui.Helper
{
    internal class ReportCreator
    {
        public readonly string Path = "report.docx";
        private Formatting titleFormat;
        private Formatting textFormat;
        public ReportCreator() 
        {
            titleFormat = new Formatting();
            titleFormat.FontFamily = new Font("Times New Roman");
            titleFormat.Size = 18d;
            titleFormat.Position = 40;
            titleFormat.Bold = true;
            titleFormat.Spacing = 0;
            textFormat = new Formatting();
            textFormat.FontFamily = new Font("Times New Roman");
            textFormat.Size = 10d;
        }
        private DocX CreateReport()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            return DocX.Create(Path);
        }

        internal void GenerateTicketReport()
        {
            var doc = CreateReport();

            var paragraph = doc.InsertParagraph("Количество и выручка от продажи билетов за тот месяц", false, titleFormat);
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
        }

        internal void GenerateUserTransactions(User user)
        {
            var doc = CreateReport();

            var paragraph = doc.InsertParagraph($"Транзакции пользователя {user.Login}", false, titleFormat);
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
        }

        internal void GenerateRouteUsers(Route route)
        {
            var doc = CreateReport();

            var paragraph = doc.InsertParagraph($"Пользователи с билетами на рейс {route.Id}", false, titleFormat);
            paragraph.LineSpacing = 15;

            var users = RequestClient.GetRouteTickets(route.Id);

            Table t = doc.AddTable(users.Count + 1, 5);
            t.Alignment = Alignment.center;

            t.Rows[0].Cells[0].Paragraphs.First().Append("Id пользователя");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Логин");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Серия паспорта");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Номер паспорта");

            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];

                t.Rows[i + 1].Cells[0].Paragraphs.First().Append($"{user.Id}");
                t.Rows[i + 1].Cells[1].Paragraphs.First().Append($"{user.Login}");
                t.Rows[i + 1].Cells[2].Paragraphs.First().Append($"{user.PassportSeries}");
                t.Rows[i + 1].Cells[3].Paragraphs.First().Append($"{user.PassportNumber}");
            }

            doc.InsertTable(t);

            doc.Save();
        }
    }
}
