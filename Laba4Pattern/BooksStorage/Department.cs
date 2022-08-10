using System.Text;

namespace library_laba4
{
    public class Department : IDepartment
    {
        public string Name { get; private set; }

        private readonly Bookсase[] _bookсases;

        public Department(string name, int bookсaseNum, int bookсaseHieght, int bookсaseWidth)
        {
            Name = name;
            _bookсases = new Bookсase[bookсaseNum];

            for (int i = 0; i < _bookсases.Length; i++)
            {
                _bookсases[i] = new Bookсase(bookсaseHieght, bookсaseWidth);
            }
        }

        public void SetBook(Book book)
        {
            int deport, shelf, cell;
            (deport, shelf, cell) = book.GetPosition();

            _bookсases[deport].SetBook(book, shelf, cell);
        }

        public Book GetBook(int deport, int shelf, int cell)
        {
            return _bookсases[deport].GetBook(shelf, cell);
        }

        public Book ShowBook(int deport, int shelf, int cell)
        {
            return _bookсases[deport].ShowBook(shelf, cell);
        }

        public string GetFullInformation()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Основная информация об отделе");

            builder.AppendLine($"Название: {Name}");

            var a = _bookсases[0].GetSize();
            builder.AppendLine($"Размер шкафов: {a.Item1} {a.Item2}");

            int i = 1;

            foreach (var bookcase in _bookсases)
            {
                foreach (var book in bookcase.ShowBooks())
                {
                    builder.AppendLine($"===== Книга {i++} =====");
                    builder.Append(book.GetFullInformation());
                    builder.AppendLine(" ");
                }
            }

            return builder.ToString();
        }

        public void Dispose()
        {
        }
    }
}