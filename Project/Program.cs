using Project;

var John = new Adult("John", "Doe", "john@example.com", "123456789", "123 Street",25,true);
var JohnPermissions = new PersonWithRoles(John);


JohnPermissions.AddEmployeeRole("Manager", DateTime.Now, 50000);
JohnPermissions.AddCustomerRole();