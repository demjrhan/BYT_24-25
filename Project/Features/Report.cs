using Project.Enum;

namespace Project.Features
{
    public class Report
    {
        private static List<Report> Instances = new List<Report>();
        private int _employeeId;
        private string _content = null!;
        private DateTime _date;
        public ReportType ReportType { get; set; }

        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                ValidateEmployeeId(value);
                _employeeId = value;
            }
        }
        
        public string Content
        {
            get => _content;
            set
            {
                ValidateContent(value);
                _content = value;
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                ValidateDate(value);
                _date = value;
            }
        }
        public Report(int employeeId, ReportType reportType, string content, DateTime date)
        {

            ValidateEmployeeId(employeeId);
            ValidateContent(content);
            ValidateDate(date);

            EmployeeId = employeeId;
            ReportType = reportType;
            Content = content;
            Date = date;

            Instances.Add(this);
        }

        public static IReadOnlyList<Report> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Report givenReport)
        {
            foreach (var report in Instances)
            {
                if (givenReport == report)
                    return true;
            }
            return false;
        }
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateEmployeeId(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(employeeId), "Employee ID must be a positive integer.");
        }

        private static void ValidateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be null or empty.", nameof(content));
        }

        private static void ValidateDate(DateTime date)
        {
            if (date > DateTime.Now)
                throw new ArgumentException("Date cannot be set to a future time.", nameof(date));
        }
        
        public static void PrintInstances()
        {
            foreach (var report in Instances)
            {
                Console.WriteLine(report.ToString());
            }
        }

        public override string ToString()
        {
            return $"Employee ID: {EmployeeId}, Report Type: {ReportType}, Date: {Date.ToShortDateString()}, Content: {Content}";
        }
    }

}

