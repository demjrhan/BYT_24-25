using Project.Enum;

namespace Project.Interfaces
{
    public interface IRetired
    {
        public bool IsRetired { get; set; }
        public RetirementType? RetirementType { get; set; }
    }

}

