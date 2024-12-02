using Project.Entities;
using Project.Enum;

namespace Project_Tests
{
    public class EmployeeTests
    {
        [Test]
        public void EmployeeCreation()
        {
            var empPosition = Position.Manager;
            var hireDate = DateTime.Now;
            double salary = 50000;

            var employee = new Employee("Kirill", "Tumoian", "Kir@mail.com", "12345", "Address", 30, false, true, false, empPosition, hireDate, salary);

            Assert.That(employee.Name, Is.EqualTo("Kirill"));
            Assert.That(employee.Surname, Is.EqualTo("Tumoian"));
            Assert.That(employee.EmpPosition, Is.EqualTo(Position.Manager));
            Assert.That(employee.Salary, Is.EqualTo(50000));
        }
    }
}
