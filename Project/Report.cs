namespace Project
{
    public enum ReportType
    {
        Daily,
        Weekly,
        Monthly
    }

    public class Report
    {
        private static List<Report> Instances = [];

        private int _employeeId;
        private string _content = null!;
        private DateTime _date;

        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Employee ID must be a positive integer.");
                _employeeId = value;
            }
        }
        public ReportType ReportType { get; set; }
        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Content cannot be null or empty.", nameof(value));
                _content = value;
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be set to a future time.", nameof(value));
                _date = value;
            }
        }

        public Report(int employeeId, ReportType reportType, string content, DateTime date)
        {
            EmployeeId = employeeId;
            ReportType = reportType;
            Content = content;
            Date = date;

            Instances.Add(this);
        }

        public static List<Report> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return "Employee id: " + EmployeeId + " type: " + ReportType + " Contents: " + Content;
        }
    }

}

