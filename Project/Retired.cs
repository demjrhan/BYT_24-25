namespace Project
{
    public enum Retirement
    {
        Military,
        HealthIssues,
        Other
    }

    public interface IRetired
    {
        public bool IsRetired { get; set; }
        public Retirement? RetirementType { get; set; }
    }

}

