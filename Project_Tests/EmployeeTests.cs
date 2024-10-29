
using Project;

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

            Assert.AreEqual("Kirill", employee.Name);
            Assert.AreEqual("Tumoian", employee.Surname);
            Assert.AreEqual(Position.Manager, employee.EmpPosition);
            Assert.AreEqual(50000, employee.Salary);
        }
    }
}
