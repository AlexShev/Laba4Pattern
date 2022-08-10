
using System;

namespace library_laba4
{
    public interface IBookBuilder
    {
        BookBuilder AddIssueRecord(DateTime dateOfIssue, int days, int userID);
        BookBuilder AddReturnRecord(DateTime dateOfIssue, int userID);
        BookBuilder SetAuthor(string author);
        BookBuilder SetBookDepartment(int department);
        BookBuilder SetBookName(string bookName);
        BookBuilder SetBookсaseNumber(int bookсaseNumber);
        BookBuilder SetCellNumber(int cellNumber);
        BookBuilder SetInstanceNumber(int instanceNumber);
        BookBuilder SetShelfNumber(int shelfNumber);
        Book Build();
    }
}