using System;

namespace library_laba4
{
    public interface IDepartment : IDisposable
    {
        string Name { get; }

        Book GetBook(int deport, int shelf, int cell);
        Book ShowBook(int deport, int shelf, int cell);

        string GetFullInformation();
        void SetBook(Book book);
    }
}