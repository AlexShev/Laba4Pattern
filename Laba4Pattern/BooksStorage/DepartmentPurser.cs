using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace library_laba4
{
    public class DepartmentPurser
    {
        private static readonly Regex namePatern = new Regex(@"Название: ([A-Z А-Я Ё a-z а-я ё 0-9 \-]+)");
        private static readonly Regex sizeBookcasePatern = new Regex(@"Размер шкафов: ([0-9]+) ([0-9]+)");

        private MetaData _metaData;

        private IDictionary<string, int> _booksNames = new Dictionary<string, int>();
        private IDictionary<int, IList<((int, int, int), bool)>> _booksPositions = new Dictionary<int, IList<((int, int, int), bool)>>();

        private Department _department;

        public DepartmentPurser(string filePath)
        {
            StreamReader fs = new StreamReader(filePath);

            fs.ReadLine();
            string name = namePatern.Match(fs.ReadLine() ?? "Название: -").Groups[1].Value;
            var sizeBookcase = sizeBookcasePatern.Match(fs.ReadLine() ?? "Размер шкафов: 10 10").Groups;

            int hieght = int.Parse(sizeBookcase[1].Value);
            int width = int.Parse(sizeBookcase[2].Value);

            var booksSource = fs.ReadToEnd().Split(' ');
            IList<Book> books = new List<Book>();

            int maxBookcaseNum = 1;

            foreach (var bookSource in booksSource)
            {
                Book book = new BookParser(bookSource, new BookBuilder()).Parse();
                books.Add(book);
                maxBookcaseNum = Math.Max(maxBookcaseNum, book.GetPosition().Item1 + 1);

                AddBookToConteiners(book);
            }

            fs.Close();

            _department = new Department(name, maxBookcaseNum, hieght, width);
            _metaData = new MetaData(_booksNames, _booksPositions);

            foreach (var book in books)
            {
                _department.SetBook(book);
            }
        }

        private void AddBookToConteiners(Book book)
        {
            if (!_booksNames.ContainsKey(book.Name))
            {
                _booksNames.Add(book.Name, _booksNames.Count);

                IList<((int, int, int), bool)> node = new List<((int, int, int), bool)>();
                node.Add((book.GetPosition(), true));
                _booksPositions.Add(_booksNames.Count - 1, node);
            }
            else
            {
                _booksPositions[_booksNames[book.Name]].Add((book.GetPosition(), true));
            }
        }

        public Department Parse()
        {
            return _department;
        }

        public MetaData GetMetaData()
        {
            return _metaData;
        }

    }
}