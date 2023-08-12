// See https://aka.ms/new-console-template for more information
using OtuGlazevTestDB;
using OtuGlazevTestDB.DataBase;
using OtuGlazevTestDB.DataBase.Entities;
using System.Globalization;

Repository _repository = new Repository();

Console.WriteLine("Добро пожаловть в терминал СБЕРа!");

while (true)
{
    start:
    Console.WriteLine("Введите нужную цифру и нажмите Enter:");
    Console.WriteLine("1 - Вывод всей информации из БД. 2 - Добавление новых данных");
    var key = Console.ReadLine();
    var num = 0;

    var success = Int32.TryParse(key, out num);
    if (!success)
    {
        Console.WriteLine("Нужно ввести цифру 1 или 2 и нажать Enter");
        goto start;
    }

    if(num == 1)
    {
        ReadAllData();
    }
    if (num == 2)
    {
        WriteData();
    }

    goto start;

}

void ReadAllData()
{

    var users = _repository.GetAllUsers();
    var credits = _repository.GetAllCredits();
    var creditTypes = _repository.GetAllCreditTypes();


    Console.WriteLine(); Console.WriteLine();
    Console.WriteLine("=======================================");
    Console.WriteLine(); Console.WriteLine();

    Console.WriteLine("Пользователи системы:");
    
    foreach (var user in users)
    {
        Console.WriteLine($"ID: {user.Id}");
        Console.WriteLine($"Имя: {user.firstName}");
        Console.WriteLine($"Фамилия: {user.lastName}");
        Console.WriteLine($"Дата рождения: {user.dob}");
        Console.WriteLine($"Кредитный рейтинг: {user.creditScore}");
        Console.WriteLine("--------------------------------------");
    }

    Console.WriteLine(); Console.WriteLine();
    Console.WriteLine("=======================================");
    Console.WriteLine(); Console.WriteLine();

    Console.WriteLine("Типы кредитов в системе:");
    foreach (var creditType in creditTypes)
    {
        Console.WriteLine($"ID : {creditType.id}");

        Console.WriteLine($"Название : {creditType.type}");
      
    }

    Console.WriteLine(); Console.WriteLine();
    Console.WriteLine("=======================================");
    Console.WriteLine(); Console.WriteLine();

    Console.WriteLine("Все кредиты в системе:");
    foreach (var credit in credits)
    {
        Console.WriteLine($"Тип кредита: {creditTypes.FirstOrDefault(m=>m.id == credit.typeId).type}");
        var user = users.FirstOrDefault(m => m.Id == credit.userId);
        Console.WriteLine($"ID заёмщика: {user.Id}");
        Console.WriteLine($"Имя заёмщика: {user.firstName}");
        Console.WriteLine($"Фамилия заёмщика: {user.lastName}");
        Console.WriteLine($"Сумма заема: {credit.debt}");
        Console.WriteLine("--------------------------------------");
    }
    Console.WriteLine(); Console.WriteLine();
    Console.WriteLine("=======================================");
    Console.WriteLine(); Console.WriteLine();
}

void WriteData()
{
    Console.WriteLine("Выберите действие: ");
    Console.WriteLine("1 - Добавить нового пользователя. 2 - Добавить новый тип кредита 3. Добавить новый кредит пользователя. 4 - В главное меню");
    var key = Console.ReadLine();
    var num = 0;

    var success = Int32.TryParse(key, out num);
    if (!success)
    {
        Console.WriteLine("Нужно ввести цифру и нажать Enter");
        WriteData();
    }

    if(num == 4)
    {
        return;
    }

    if (num == 1)
    {
        Console.WriteLine("Введите имя пользователя: ");
        var fname = Console.ReadLine();

        Console.WriteLine("Введите фамилию пользователя: ");
        var lname = Console.ReadLine();

        Console.WriteLine("Введите дату рождения пользователя в формате dd.MM.yyyy (напр. 14.01.2002): ");
        var dob = Console.ReadLine();


        var user = new User();
        user.firstName = fname;
        user.lastName = lname;
        var local = DateTime.ParseExact(dob, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        user.dob = DateTime.SpecifyKind(local, DateTimeKind.Utc);
        
        user.creditScore = 100;

        try
        {
            _repository.AddNewUser(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового пользователя: {ex.Message}");
            return;
        }

        Console.WriteLine("Пользователь успешно создан!");
    }

    if (num == 2)
    {
        Console.WriteLine("Введите название типа кредита: ");
        var title = Console.ReadLine();

        var creditType = new CreditType();
        creditType.type = title;
     

        try
        {
            _repository.AddNewCreditType(creditType);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового типа кредита: {ex.Message}");
            return;
        }

        Console.WriteLine("Тип кредита успешно создан!");
    }

    if (num == 3)
    {
        Console.WriteLine("Введите ID типа кредита: ");
        var creditId = Console.ReadLine();

        Console.WriteLine("Введите ID заемщика: ");
        var userCreditId = Console.ReadLine();

        Console.WriteLine("Введите сумму заема: ");
        var creditDebt = Console.ReadLine();

        var credit = new Credit();
        credit.debt = Convert.ToInt32(creditDebt);
        credit.userId = Convert.ToInt32(userCreditId);
        credit.typeId = Convert.ToInt32(creditId);

        try
        {
            _repository.AddNewCredit(credit);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового кредита: {ex.Message}");
            return;
        }

        Console.WriteLine("Кредит успешно создан!");
    }
}