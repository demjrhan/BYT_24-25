namespace Project
{
    public static class Controller
    {
        private static bool _clearConsole = false;
        private static int _currentMenuId = 0;
        private static readonly string _header =
            "**************************************************\n" +
            "***** Bookshop CLI Controller ********************\n" +
            "***** Choose an option and type it's number. *****\n" +
            "**************************************************";
        private static readonly string _mainMenu =
            "***** 1 - Employees ******************************\n" +
            "***** 2 - Customers ******************************\n" +
            "***** 3 - Products *******************************\n" +
            "***** 4 - (De)Serialization **********************\n" +
            "***** 5 - Check inventory ************************\n" +
            "**************************************************";
        private static readonly string _employeesMenu =
            "***** 1 - List all employees *********************\n" +
            "***** 2 - Add new employee ***********************\n" +
            "***** 3 - !Delete employee ***********************\n" +
            "***** 4 - !Modify information ********************\n" +
            "***** 5 - Go back ********************************\n" +
            "**************************************************";
        private static readonly string _customersMenu =
            "***** 1 - List all customers *********************\n" +
            "***** 2 - Add new customer ***********************\n" +
            "***** 3 - !Delete customer ***********************\n" +
            "***** 4 - !Modify information ********************\n" +
            "***** 5 - Go back ********************************\n" +
            "**************************************************";
        private static readonly string _productsMenu =
            "***** 1 - List all books *************************\n" +
            "***** 2 - List all accessories *******************\n" +
            "***** 3 - Add new book ***************************\n" +
            "***** 4 - Add new accessory **********************\n" +
            "***** 5 - Add new promotion **********************\n" +
            "***** 6 - !Remove promotion **********************\n" +
            "***** 7 - Go back ********************************\n" +
            "**************************************************";
        private static readonly string _promotionSubMenu =
            "***** 1 - Add promotion to a book ****************\n" +
            "***** 2 - Add promotion to accessory *************\n" +
            "**************************************************";
        private static readonly string _persistenceMenu =
            "***** 1 - Serialize data *************************\n" +
            "***** 2 - Deserialize data ***********************\n" +
            "***** 3 - Go back ********************************\n" +
            "**************************************************";
        //private static readonly string _serializeSubMenu =
        //    "***** 1 - Employees ******************************\n" +
        //    "***** 2 - Customers ******************************\n" +
        //    "***** 3 - Products *******************************\n" +
        //    "**************************************************";

        public static void Start()
        {
            Console.WriteLine("Do you want to enable Clear Console method?");
            Console.WriteLine("y for Yes; n for No");
            _clearConsole = Console.ReadLine() == "y";
            Console.Clear();

            while (true)
            { 
                Console.WriteLine(_header);

                switch (_currentMenuId)
                {
                    case 0:
                        HandleMainMenu();
                        break;
                    case 1:
                        HandleEmployeesMenu();
                        break;
                    case 2:
                        HandleCustomersMenu();
                        break;
                    case 3:
                        HandleProductsMenu();
                        break;
                    case 4:
                        HandlePersistenceMenu();
                        break;
                }
            }

        }

        public static void HandleMainMenu()
        {
            Console.WriteLine(_mainMenu);

            string? input = Console.ReadLine();
            int option = Convert.ToInt32(input);

            if (option == 5)
            {
                Inventory.InventoryInfo();
                return;
            }

            _currentMenuId = option;
        }

        public static void HandleEmployeesMenu()
        {
            Console.WriteLine(_employeesMenu);

            string? input = Console.ReadLine();

            int option = Convert.ToInt32(input);

            switch(option)
            {
                case 1:
                    foreach (var e in Employee.GetInstances())
                    {
                        Console.WriteLine(e.ToString());
                    }
                    break;
                case 2:
                    Console.WriteLine("Write name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Write surname");
                    string? surname = Console.ReadLine();
                    Console.WriteLine("Write email");
                    string? email = Console.ReadLine();
                    Console.WriteLine("Write phone");
                    string? phone = Console.ReadLine();
                    Console.WriteLine("Write address");
                    string? address = Console.ReadLine();
                    Console.WriteLine("Write age");
                    string? inputAge = Console.ReadLine();
                    int age = Convert.ToInt32(inputAge);
                    Console.WriteLine("Is person studying? y - yes; n - no");
                    bool isStudying = Console.ReadLine() == "y";
                    bool isWorking = true;
                    bool isRetired = false;
                    Position position = Position.Assistant;
                    Console.WriteLine("Choose position: ");
                    Console.WriteLine("1 - Manager");
                    Console.WriteLine("2 - Assistant");
                    string? inputPos = Console.ReadLine();
                    int pos = Convert.ToInt32(inputPos);
                    switch(pos)
                    {
                        case 1:
                            position = Position.Manager; break;
                        case 2:
                            position = Position.Assistant; break;
                    }
                    DateTime hireDate = DateTime.Now;
                    Console.WriteLine("Write salary");
                    string? inputSalary = Console.ReadLine();
                    double salary = Convert.ToDouble(inputSalary);

                    if (
                        name != null &&
                        surname != null &&
                        email != null &&
                        phone != null &&
                        address != null)
                        new Employee(
                        name, surname, email, phone, 
                        address, age, isStudying,
                        isWorking, isRetired,
                        position, hireDate, salary);
                    ClearLog();
                    Console.WriteLine("Employee was added.");
                    break;
                case 5:
                    _currentMenuId = 0; break;
            }
        }

        public static void HandleCustomersMenu()
        {
            Console.WriteLine(_customersMenu);

            string? input = Console.ReadLine();

            int option = Convert.ToInt32(input);

            switch (option)
            {
                case 1:
                    foreach (var c in Customer.GetInstances())
                    {
                        Console.WriteLine(c.ToString());
                    }
                    break;
                case 2:
                    Console.WriteLine("Write name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Write surname");
                    string? surname = Console.ReadLine();
                    Console.WriteLine("Write email");
                    string? email = Console.ReadLine();
                    Console.WriteLine("Write phone");
                    string? phone = Console.ReadLine();
                    Console.WriteLine("Write address");
                    string? address = Console.ReadLine();
                    Console.WriteLine("Write age");
                    string? inputAge = Console.ReadLine();
                    int age = Convert.ToInt32(inputAge);
                    Console.WriteLine("Is person studying? y - yes; n - no");
                    bool isStudying = Console.ReadLine() == "y";
                    Console.WriteLine("Is person working? y - yes; n - no");
                    bool isWorking = Console.ReadLine() == "y";
                    Console.WriteLine("Is person retired? y - yes; n - no");
                    bool isRetired = Console.ReadLine() == "y";
                    Retirement? retirement = null;
                    if (isRetired)
                    {
                        Console.WriteLine("Choose type of retirement: ");
                        Console.WriteLine("1 - Health Issues");
                        Console.WriteLine("2 - Military");
                        Console.WriteLine("3 - Other");
                        string? inputRetire = Console.ReadLine();
                        int retire = Convert.ToInt32(inputRetire);
                        switch (retire)
                        {
                            case 1:
                                retirement = Retirement.HealthIssues;
                                break;
                            case 2:
                                retirement = Retirement.Military;
                                break;
                            case 3:
                                retirement = Retirement.Other;
                                break;
                        }
                    }

                    if (
                        name != null && 
                        surname != null && 
                        email != null &&
                        phone != null &&
                        address != null)
                        new Customer(
                            name, surname, email, phone,
                            address, age, isStudying,
                            isWorking, isRetired,
                            retirement);
                    ClearLog();
                    Console.WriteLine("Customer was added.");
                    break;
                case 5:
                    ClearLog();
                    _currentMenuId = 0; return;
            }
        }

        public static void HandleProductsMenu()
        {
            Console.WriteLine(_productsMenu);

            string? input = Console.ReadLine();

            int option = Convert.ToInt32(input);

            switch (option)
            {
                case 1:
                    foreach (var b in Book.GetInstances())
                    {
                        Console.WriteLine(b.ToString());
                    }
                    break;
                case 2:
                    foreach (var a in Accessory.GetInstances())
                    {
                        Console.WriteLine(a.ToString());
                    }
                    break;
                case 3:
                    Console.WriteLine("Write title");
                    string? title = Console.ReadLine();
                    Console.WriteLine("Write author");
                    string? author = Console.ReadLine();
                    Console.WriteLine("Write genre");
                    string? genre = Console.ReadLine();
                    Console.WriteLine("Write publication year");
                    string? inputYear = Console.ReadLine();
                    int year = Convert.ToInt32(inputYear);
                    Console.WriteLine("Write price");
                    string? inputPrice = Console.ReadLine();
                    double price = Convert.ToDouble(inputPrice);
                    Console.WriteLine("Write books amount");
                    string? inputAmount = Console.ReadLine();
                    int quantity = Convert.ToInt32(inputAmount);

                    if (title != null &&
                        author != null &&
                        genre != null)
                        new Book(
                        title, price, quantity, author,
                        genre, year);
                    ClearLog();
                    Console.WriteLine("Book was added.");
                    break;
                case 4:
                    Console.WriteLine("Write name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Write type");
                    string? type = Console.ReadLine();
                    Console.WriteLine("Choose material type: ");
                    Console.WriteLine("1 - Metal");
                    Console.WriteLine("2 - Plastic");
                    Console.WriteLine("3 - Wood");
                    Console.WriteLine("4 - Gold");
                    Console.WriteLine("5 - Leather");
                    MaterialType materialType = MaterialType.Metal;
                    string? inputMaterial = Console.ReadLine();
                    int material = Convert.ToInt32(inputMaterial);
                    switch (material)
                    {
                        case 1:
                            materialType = MaterialType.Metal;
                            break;
                        case 2:
                            materialType = MaterialType.Plastic;
                            break;
                        case 3:
                            materialType = MaterialType.Wood;
                            break;
                        case 4:
                            materialType = MaterialType.Gold;
                            break;
                        case 5:
                            materialType = MaterialType.Leather;
                            break;
                    }
                    Console.WriteLine("Write price");
                    string? inputPriceAcc = Console.ReadLine();
                    double priceAcc = Convert.ToDouble(inputPriceAcc);
                    Console.WriteLine("Write accessories amount");
                    string? inputAmountAcc = Console.ReadLine();
                    int quantityAcc = Convert.ToInt32(inputAmountAcc);

                    if (name != null &&
                        type != null)
                        new Accessory(
                        name, priceAcc, quantityAcc, type,
                        materialType);
                    ClearLog();
                    Console.WriteLine("Accessory was added.");
                    break;
                case 5:
                    HandlePromotionSubMenu(); break;
                case 7:
                    ClearLog();
                    _currentMenuId = 0; break;
            }
        }

        public static void HandlePromotionSubMenu()
        {
            Console.WriteLine(_header);
            Console.WriteLine(_promotionSubMenu);
            
            string? input = Console.ReadLine();
            int option = Convert.ToInt32(input);

            Console.WriteLine("Write promotion title");
            string? promTitle = Console.ReadLine();
            Console.WriteLine("Write description");
            string? promDescr = Console.ReadLine();
            Console.WriteLine("Write discount");
            string? discInput = Console.ReadLine();
            double disc = Convert.ToDouble(discInput);

            switch (option)
            {
                case 1:
                    foreach (var b in Book.GetInstances())
                    {
                        Console.WriteLine(b.ToString());
                    }

                    Console.WriteLine("Type book id");
                    string? idInput = Console.ReadLine();
                    int idOption = Convert.ToInt32(idInput);

                    Book book = Book.GetInstances()[idOption];

                    foreach (var b in Book.GetInstances())
                    {
                        if (b.ProductId == idOption) book = b; break;
                    }

                    if (promTitle != null &&
                        promDescr != null)
                        book.AddPromotion(promTitle, promDescr, disc);
                    Console.WriteLine("Promotion was added");
                    break;
                case 2:
                    foreach (var a in Accessory.GetInstances())
                    {
                        Console.WriteLine(a.ToString());
                    }

                    Console.WriteLine("Type book id");
                    string? idInputAcc = Console.ReadLine();
                    int idOptionAcc = Convert.ToInt32(idInputAcc);

                    Accessory accessory = Accessory.GetInstances()[idOptionAcc];

                    foreach (var a in Accessory.GetInstances())
                    {
                        if (a.ProductId == idOptionAcc) accessory = a; break;
                    }

                    if (promTitle != null &&
                        promDescr != null)
                        accessory.AddPromotion(promTitle, promDescr, disc);
                    Console.WriteLine("Promotion was added");
                    break;
            }
        }

        public static void HandlePersistenceMenu()
        {
            Console.WriteLine(_persistenceMenu);

            string? input = Console.ReadLine();
            int option = Convert.ToInt32(input);

            switch (option)
            {
                case 1:
                    SerializeDeserialize.SerializeToFile(); break;
                case 2:
                    SerializeDeserialize.DeserializeFromFile(); break;
                case 3:
                    ClearLog();
                    _currentMenuId = 0; break;
            }
        }

        //public static void HandleSerializeSubMenu()
        //{
        //    Console.WriteLine(_header);
        //    Console.WriteLine(_serializeSubMenu);

        //    string? input = Console.ReadLine();
        //    int option = Convert.ToInt32(input);

        //    switch(option)
        //    {
        //        case 1:
        //            SerializeDeserialize.SerializeToFile(
        //                Employee.Employees, "employees.json");
        //            break;
        //        case 2:
        //            SerializeDeserialize.SerializeToFile(
        //                Customer.Customers, "customers.json");
        //            break;
        //        case 3:
        //            SerializeDeserialize.SerializeProducts();
        //            break;
        //    }
        //}

        //public static void HandleDeSerializeSubMenu()
        //{
        //    Console.WriteLine(_header);
        //    Console.WriteLine(_serializeSubMenu);

        //    string? input = Console.ReadLine();
        //    int option = Convert.ToInt32(input);

        //    switch (option)
        //    {
        //        case 1:
        //            List<Employee> employees = 
        //                SerializeDeserialize.DeserializeFromFile<Employee>("employees.json");
        //            foreach (var e in employees)
        //            {
        //                Console.WriteLine(e.ToString());
        //            }
        //            break;
        //        case 2:
        //            List<Customer> customers = 
        //                SerializeDeserialize.DeserializeFromFile<Customer>("customers.json");
        //            foreach (var c in customers)
        //            {
        //                Console.WriteLine(c.ToString());
        //            }
        //            break;
        //        case 3:
        //            SerializeDeserialize.DeserializeProducts();
        //            break;
        //    }
        //}

        public static void ClearLog()
        {
            if (_clearConsole) Console.Clear();
            Console.WriteLine();
        }
    }
}
