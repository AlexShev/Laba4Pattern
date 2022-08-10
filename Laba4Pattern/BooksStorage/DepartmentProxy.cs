using library_laba4.Logers;
using System;

namespace library_laba4
{
    public class DepartmentProxy : IDepartment, IDisposable
    {
        private readonly Department _department;
        private readonly ILoger _loger;

        public DepartmentProxy(Department department, ILoger loger)
        {
            _loger = loger;
            _department = department;
            _loger.Log(_department.GetFullInformation());
        }

        public string Name => _department.Name;

        public void Dispose()
        {
            _loger.Log(_department.GetFullInformation());
        }

        public Book GetBook(int deport, int shelf, int cell)
        {
            var book = _department.GetBook(deport, shelf, cell);
            _loger.Log($"Выдана книга {book?.ToString() ?? "-"}");

            return book;
        }

        public Book ShowBook(int deport, int shelf, int cell)
        {
            return _department.ShowBook(deport, shelf, cell);
        }

        public string GetFullInformation()
        {
            _loger.Log(_department.GetFullInformation());
            return _department.GetFullInformation();
        }

        public void SetBook(Book book)
        {
            _loger.Log($"Получена книга {book}");

            _department.SetBook(book);
        }
    }
}