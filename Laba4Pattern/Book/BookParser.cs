using System;
using System.Text.RegularExpressions;

namespace library_laba4
{
    public class BookParser
    {
        // паттерны
        private static readonly Regex nameAndAthorPatern = new Regex(@"Название: ([A-Z А-Я Ё a-z а-я ё 0-9 \-]+), Автор: ([A-Z А-Я Ё a-z а-я ё 0-9 \-]+)");
        private static readonly Regex positionPatern = new Regex(@"Положение: № отдела: ([0-9]+),№ шкафа: ([0-9]+), № полки: ([0-9]+), № ячейки: ([0-9]+)");
        private static readonly Regex numberPatern = new Regex(@"Количество экземпляров: ([0-9]+)");

        private static readonly Regex issuePatern = new Regex(@"Дата выдачи: (\d{2}.\d{2}.\d{4}) Количество дней: (\d+) Номер Читательского билета: ([0-9]+)");
        private static readonly Regex returnPatern = new Regex(@"Дата возврата: (\d{2}.\d{2}.\d{4}) Номер Читательского билета: ([0-9]+)");

        //private static readonly string ReaderID_1 = @"([0-9 a-z]{8}-[0-9 a-z]{4}-[0-9 a-z]{4}-[0-9 a-z]{4}-[0-9 a-z]{12})";
        //private static readonly string ReaderID_2 = @"([0-9]+)";

        // поля
        private readonly IBookBuilder _builder;

        public BookParser(string src, IBookBuilder builder)
        {
            _builder = builder;

            ParseBookCard(nameAndAthorPatern.Match(src),
                positionPatern.Match(src),
                numberPatern.Match(src));

            ParseHistory(issuePatern.Matches(src),
                returnPatern.Matches(src));
        }

        public Book Parse()
        {
            return _builder.Build();
        }

        private void ParseBookCard(Match nameAndAthor, Match position, Match number)
        {
            _builder.SetBookName(nameAndAthor.Groups[1].Value)
                .SetAuthor(nameAndAthor.Groups[2].Value)
                .SetBookDepartment(int.Parse(position.Groups[1].Value))
                .SetBookсaseNumber(int.Parse(position.Groups[2].Value))
                .SetShelfNumber(int.Parse(position.Groups[3].Value))
                .SetCellNumber(int.Parse(position.Groups[4].Value))
                .SetInstanceNumber(int.Parse(number.Groups[1].Value));
        }

        private void ParseHistory(MatchCollection issues, MatchCollection returns)
        {
            for (int i = 0; i < issues.Count; i++)
            {
                _builder.AddIssueRecord(DateTime.Parse(issues[i].Groups[1].Value),
                    int.Parse(issues[i].Groups[2].Value),
                    int.Parse(issues[i].Groups[3].Value)
                );
            }

            for (int i = 0; i < returns.Count; i++)
            {
                _builder.AddReturnRecord(DateTime.Parse(returns[i].Groups[1].Value),
                    int.Parse(returns[i].Groups[2].Value)
                );
            }
        }
    }
}