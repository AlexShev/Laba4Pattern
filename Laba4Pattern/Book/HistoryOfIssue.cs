using System;
using System.Collections.Generic;
using System.Text;

namespace library_laba4
{
    public class HistoryOfIssue
    {
        private IList<(DateTime, int, int)> _issueRecord;
        private IList<(DateTime, int)> _returnRecord;

        public HistoryOfIssue()
        {
            _issueRecord = new List<(DateTime, int, int)>();
            _returnRecord = new List<(DateTime, int)>(); ;
        }

        public void IssueRecord(DateTime dateOfIssue, int days, int userID)
        {
            _issueRecord.Add((dateOfIssue, days, userID));
        }

        public void IssueRecord((DateTime, int, int) record)
        {
            _issueRecord.Add(record);
        }

        public void ReturnRecord(DateTime dateOfIssue, int userID)
        {
            _returnRecord.Add((dateOfIssue, userID));
        }

        public void ReturnRecord((DateTime, int) record)
        {
            _returnRecord.Add(record);
        }

        public string GetHistory()
        {
            StringBuilder builder = new StringBuilder();

            int i = 0;

            for (; i < _returnRecord.Count; i++)
            {
                AppendIssue(builder, i);
                AppendRecord(builder, i);
            }

            if (i == _issueRecord.Count - 1)
            {
                AppendIssue(builder, i);
            }

            return builder.ToString();
        }

        private void AppendIssue(StringBuilder builder, int i)
        {
            builder.Append("Дата выдачи: ")
                .Append(_issueRecord[i].Item1.ToShortDateString())
                .Append(" Количество дней: ")
                .Append(_issueRecord[i].Item2)
                .Append(" Номер Читательского билета: ")
                .Append(string.Format("{0:d4}", _issueRecord[i].Item3))
                .AppendLine();
        }

        private void AppendRecord(StringBuilder builder, int i)
        {
            builder.Append("Дата возврата: ")
                .Append(_returnRecord[i].Item1.ToShortDateString())
                .Append(" Номер Читательского билета: ")
                .Append(string.Format("{0:d4}", _returnRecord[i].Item2))
                .AppendLine();
        }

        public override string ToString()
        {
            return GetHistory();
        }
    }
}