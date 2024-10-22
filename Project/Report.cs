namespace Project
{
    public class Report
    {
        public int EmployeeId { get; set; }
        public string ReportType { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Report(int employeeId, string reportType, string content, DateTime date)
        {
            EmployeeId = employeeId;
            ReportType = reportType;
            Content = content;
            Date = date;
        }

        public override string ToString()
        {
            return "Employee id: " + EmployeeId + " type: " + ReportType + " Contents: " + Content;
        }
    }

}

