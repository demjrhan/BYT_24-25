
using Project.Entities;
using Project.Enum;

namespace Project_Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [SetUp]
        public void Setup()
        {
            Employee.Instances.Clear(); 
        }

        [Test]
        public void EmployeeCreation_ShouldInitializeCorrectly()
        {
           
            var hireDate = new DateTime(2020, 1, 1);
            var position = Position.Manager;

            
            var employee = new Employee(
                "Jane", "Doe", "jane.doe@example.com", "123456789",
                "456 Oak St", 30, false, true, false, 
                position, hireDate, 50000);

            
            Assert.IsNotNull(employee);
            Assert.That(employee.Name, Is.EqualTo("Jane"));
            Assert.That(employee.EmpPosition, Is.EqualTo(position));
            Assert.That(employee.HireDate, Is.EqualTo(hireDate));
            Assert.That(employee.Salary, Is.EqualTo(50000));
            Assert.IsTrue(Employee.Instances.Contains(employee));
        }

        [Test]
        public void EmployeeCreation_InvalidAge_ShouldThrowException()
        {
            
            var hireDate = new DateTime(2020, 1, 1);
            var position = Position.Manager;

         
            Assert.Throws<ArgumentException>(() =>
            {
                new Employee(
                    "John", "Doe", "john.doe@example.com", "123456789",
                    "123 Elm St", -1, false, true, false,
                    position, hireDate, 60000);
            });
        }

        [Test]
        public void EmployeeCreation_InvalidSalary_ShouldThrowException()
        {
            
            var hireDate = new DateTime(2020, 1, 1);
            var position = Position.Manager;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Employee(
                    "John", "Doe", "john.doe@example.com", "123456789",
                    "123 Elm St", 30, false, true, false,
                    position, hireDate, -1000); 
            });
        }

        [Test]
        public void EmployeeDeletion_ShouldRemoveFromInstances()
        {
            
            var hireDate = new DateTime(2020, 1, 1);
            var position = Position.Manager;

            var employee = new Employee(
                "Jane", "Doe", "jane.doe@example.com", "123456789",
                "456 Oak St", 30, false, true, false,
                position, hireDate, 50000);

            
            Employee.RemoveEmployee(employee);

            
            Assert.IsFalse(Employee.Instances.Contains(employee));
        }

        [Test]
        public void EmployeeCreation_InvalidHireDate_ShouldThrowException()
        {
            
            var hireDate = DateTime.Now.AddYears(1);
            var position = Position.Manager;

            
            Assert.Throws<ArgumentException>(() =>
            {
                new Employee(
                    "John", "Doe", "john.doe@example.com", "123456789",
                    "123 Elm St", 30, false, true, false,
                    position, hireDate, 60000);
            });
        }
    }
}
