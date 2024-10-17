namespace Project;

public class Report
{
    public string ReportType { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    public Report(string reportType, string content, DateTime date)
    {
        ReportType = reportType;
        Content = content;
        Date = date;
    }
}
