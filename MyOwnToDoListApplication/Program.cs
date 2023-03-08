// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using MyOwnToDoListApplication.Srvices;
using MyOwnToDoListApplicationLibrary;

Console.WriteLine("Hello, World!");

var builder = new ConfigurationBuilder();

builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");

var config = builder.Build();

string connectionString = config.GetConnectionString("DefaultConnection");

int menu = -3;
string? input;
ToDoService todoService = new ToDoService();
SubToDoService subtodoService = new SubToDoService();
List<ToDo> todos;
List<SubToDo> subtodos;

while (menu != 123)
{
    Console.Clear();
    todos = todoService.GetAllToDo(connectionString);

    Console.WriteLine("Choose option:");
    Console.WriteLine("1) Create ToDo");
    Console.WriteLine("2) Delete ToDO");
    Console.WriteLine("3) Update ToDo(set name)");
    Console.WriteLine("4) Get All ToDo");
    Console.WriteLine("5) Get ToDo by name");
    Console.WriteLine("6) Create SubToDo");
    Console.WriteLine("7) Delete SubToDo");
    Console.WriteLine("8) Update SubToDo(set name, description, deadline)");
    Console.WriteLine("9) Change SubToDo status");
    Console.WriteLine("10) Get all SubToDo(single ToDo)");
    Console.WriteLine("11) Exit");

    input = Console.ReadLine();

    if (!int.TryParse(input, out menu) || string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Input incorrect. Try Again");
        continue;
    }

    Console.Clear();

    switch (menu)
    {
        case 1:
            {
                Console.Write("Enter ToDo name: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todos.Find(t => t.Name == name);

                    if (tempToDo != null)
                    {
                        Console.WriteLine($"ToDo {name} already exists");
                    }
                    else
                    {
                        todoService.CreateToDo(name, 0, connectionString);
                        Console.WriteLine("New ToDo successfully created");
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 2:
            {
                Console.Write("Enter ToDo name which you want to delete: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todos.Find(t => t.Name == name);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {name}");
                    }
                    else
                    {
                        var tempSubToDoList = subtodoService.GetAllSubToDo(tempToDo.Id, connectionString);

                        foreach (var subT in tempSubToDoList)
                        {
                            subtodoService.DeleteSubToDoByName(subT.Name, connectionString);
                        }

                        todoService.DeleteToDoByName(name, connectionString);
                        Console.WriteLine($"ToDo {name} successfully deleted");
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 3:
            {
                Console.Write("Enter ToDo name which you want to update: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todos.Find(t => t.Name == name);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {name}");
                    }
                    else
                    {
                        Console.Write($"Enter new name for ToDo {name}: ");
                        string? newName = Console.ReadLine();

                        if (string.IsNullOrEmpty(newName))
                        {
                            Console.WriteLine($"NewName can't be null or empty. Name ToDo {name} didn't change");
                        }
                        else
                        {
                            todoService.UpdateToDoByName(name, newName, connectionString);
                            Console.WriteLine("ToDo successfeully updated");
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 4:
            {
                if (todos.Count == 0)
                {
                    Console.WriteLine("--- ToDo list is empty ---");
                }
                else
                {
                    foreach (var t in todos)
                    {
                        Console.WriteLine($"Id: {t.Id} - Name: {t.Name} - Progress: {t.Progress}");
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 5:
            {
                if (todos.Count == 0)
                {
                    Console.WriteLine("--- ToDo list is empty ---");
                }
                else
                {
                    Console.Write("Enter ToDo name which you want to get: ");
                    string? name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("ToDo name can't be null or empty");
                    }
                    else
                    {
                        ToDo? tempToDo = todos.Find(t => t.Name == name);

                        if (tempToDo == null)
                        {
                            Console.WriteLine($"There is no ToDO {name}");
                            Console.WriteLine("Maybe you enter wrong name or make mistake?");
                        }
                        else
                        {
                            Console.WriteLine($"Id: {tempToDo.Id} - Name: {tempToDo.Name} - Progress: {tempToDo.Progress}");

                            subtodos = subtodoService.GetAllSubToDo(tempToDo.Id, connectionString);

                            if (subtodos.Count == 0)
                            {
                                Console.WriteLine("\t--- SubToDo list is empty ---");
                            }
                            else
                            {
                                foreach (var sub in subtodos)
                                {
                                    Console.Write($"\tId: {sub.Id} - Name: {sub.Name} - Description: {sub.Description} - Deadline: {sub.Deadline} - ");
                                    if (sub.Status)
                                    {
                                        Console.WriteLine("Status: completed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Status: in progress");
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 6:
            {
                Console.Write("Enter ToDo name in which you want to create SubToDo: ");
                string? todoName = Console.ReadLine();

                if (string.IsNullOrEmpty(todoName))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todoService.GetToDoByName(todoName, connectionString);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {todoName}");
                    }
                    else
                    {
                        Console.Write("Enter SubToDo name: ");
                        string? name = Console.ReadLine();

                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("SubToDo name can't be null or empty");
                        }
                        else
                        {
                            SubToDo? tempSubToDo = subtodoService.GetSubToDoByName(name, connectionString);

                            if (tempSubToDo == null)
                            {
                                Console.WriteLine($"SubToDo {name} already exists");
                            }
                            else
                            {
                                Console.Write("Enter SubToDo description: ");
                                string? desc = Console.ReadLine();

                                if (string.IsNullOrEmpty(desc))
                                {
                                    desc = "*No description*";
                                }

                                Console.Write("Enter SubToDo deadline(DateTime Format: MM/DD/YYYY HH:MM:SS): ");
                                string? deadlineString = Console.ReadLine();
                                DateTime deadline;

                                if (!DateTime.TryParse(deadlineString, out deadline))
                                {
                                    Console.WriteLine("Incorrect DateTime Format. Deadline set is today.");
                                    deadline = DateTime.Today;
                                }

                                subtodoService.CreateSubToDo(name, desc, deadline, false, tempToDo.Id, connectionString);
                                Console.WriteLine("New SubToDo successfully created");
                            }
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 7:
            {
                Console.Write("Enter ToDo name in which the SubToDo is located: ");
                string? todoName = Console.ReadLine();

                if (string.IsNullOrEmpty(todoName))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todos.Find(t => t.Name == todoName);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {todoName}");
                    }
                    else
                    {
                        Console.Write("Enter SubToDo name which you want to delete: ");
                        string? subToDoName = Console.ReadLine();

                        if (string.IsNullOrEmpty(subToDoName))
                        {
                            Console.WriteLine("SubToDo name can't be null or empty");
                        }
                        else
                        {
                            SubToDo? tempSubToDo = subtodoService.GetSubToDoByName(subToDoName, connectionString);

                            if (tempSubToDo == null)
                            {
                                Console.WriteLine($"There is no SubToDo {subToDoName}");
                            }
                            else if (tempSubToDo.ToDoId != tempToDo.Id)
                            {
                                Console.WriteLine($"There is no SubToDo {subToDoName} in ToDo {todoName}");
                            }
                            else
                            {
                                subtodoService.DeleteSubToDoByName(subToDoName, connectionString);
                                Console.WriteLine($"SubToDo {subToDoName} successfully deleted");
                            }
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 8:
            {
                Console.Write("Enter ToDo name in which the SubToDo is located: ");
                string? todoName = Console.ReadLine();

                if (string.IsNullOrEmpty(todoName))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    // TODO Need additional verification
                    ToDo? tempToDo = todoService.GetToDoByName(todoName, connectionString);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {todoName}");
                    }
                    else
                    {
                        Console.Write("Enter SubToDo name which you want to update: ");
                        string? subToDoName = Console.ReadLine();

                        if (string.IsNullOrEmpty(subToDoName))
                        {
                            Console.WriteLine("SubToDo name can't be null or empty");
                        }
                        else
                        {
                            SubToDo? tempSubToDo = subtodoService.GetSubToDoByName(subToDoName, connectionString);

                            if (tempSubToDo == null)
                            {
                                Console.WriteLine($"There is no SubToDo {subToDoName}");
                            }
                            else if (tempSubToDo.ToDoId != tempToDo.Id)
                            {
                                Console.WriteLine($"There is no SubToDo {subToDoName} in ToDo {todoName}");
                            }
                            else
                            {
                                string? newName;
                                string? newDesc;
                                string? newDeadlineString;
                                DateTime newDeadline;
                                string? updateChoice;
                                Console.Write("Do you want to update name?(y/n)");
                                updateChoice = Console.ReadLine();

                                if (string.IsNullOrEmpty(updateChoice))
                                {
                                    Console.WriteLine("You didn't choose 'y' or 'n'. So input = 'n'");
                                    updateChoice = "n";
                                }

                                updateChoice = updateChoice.ToLower();
                                if (string.Equals(updateChoice, "y"))
                                {
                                    Console.Write("Enter new SubToDo name: ");
                                    newName = Console.ReadLine();

                                    if (string.IsNullOrEmpty(newName))
                                    {
                                        Console.WriteLine("New name can't be null or empty. So SubToDo name doesn't changed");
                                        newName = tempSubToDo.Name;
                                    }
                                }
                                else if (string.Equals(updateChoice, "n"))
                                {
                                    newName = tempSubToDo.Name;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input. Name won't change");
                                    newName = tempSubToDo.Name;
                                }

                                Console.Write("Do you want to update description?(y/n)");
                                updateChoice = Console.ReadLine();

                                if (string.IsNullOrEmpty(updateChoice))
                                {
                                    Console.WriteLine("You didn't choose 'y' or 'n'. So input = 'n'");
                                    updateChoice = "n";
                                }

                                updateChoice = updateChoice.ToLower();
                                if (string.Equals(updateChoice, "y"))
                                {
                                    Console.Write("Enter new SubToDo description: ");
                                    newDesc = Console.ReadLine();

                                    if (string.IsNullOrEmpty(newDesc))
                                    {
                                        Console.WriteLine("New description can't be null or empty. So SubToDo description doesn't changed");
                                        newDesc = tempSubToDo.Description;
                                    }
                                }
                                else if (string.Equals(updateChoice, "n"))
                                {
                                    newDesc = tempSubToDo.Description;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input. Description won't change");
                                    newDesc = tempSubToDo.Description;
                                }

                                Console.Write("Do you want to update deadline?(y/n)");
                                updateChoice = Console.ReadLine();

                                if (string.IsNullOrEmpty(updateChoice))
                                {
                                    Console.WriteLine("You didn't choose 'y' or 'n'. So input = 'n'");
                                    updateChoice = "n";
                                }

                                updateChoice = updateChoice.ToLower();
                                if (string.Equals(updateChoice, "y"))
                                {
                                    Console.Write("Enter new SubToDo deadline(DateTime Format: MM/DD/YYYY HH:MM:SS): ");
                                    newDeadlineString = Console.ReadLine();

                                    if (!DateTime.TryParse(newDeadlineString, out newDeadline))
                                    {
                                        Console.WriteLine("Incorrect DateTime Format. So deadline won't change");
                                        newDeadline = tempSubToDo.Deadline;
                                    }
                                }
                                else if (string.Equals(updateChoice, "n"))
                                {
                                    newDeadline = tempSubToDo.Deadline;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input. So SubToDo deadline won't change");
                                    newDeadline = tempSubToDo.Deadline;
                                }

                                subtodoService.UpdateSubToDoByName(subToDoName, newName, newDesc, newDeadline, connectionString);
                                Console.WriteLine($"SubToDo {subToDoName} information successfully updated");
                            }
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 9:
            {
                Console.Write("Enter ToDo name which SubToDo status you want to update: ");
                string? todoName = Console.ReadLine();

                if (string.IsNullOrEmpty(todoName))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todoService.GetToDoByName(todoName, connectionString);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {todoName}");
                    }
                    else
                    {
                        Console.Write("Enter SubToDo name which status you want to update: ");
                        string? subtodoName = Console.ReadLine();

                        if (string.IsNullOrEmpty(subtodoName))
                        {
                            Console.WriteLine("SubToDo name can't be null or empty");
                        }
                        else
                        {
                            SubToDo? tempSubToDo = subtodoService.GetSubToDoByName(subtodoName, connectionString);

                            if (tempSubToDo == null)
                            {
                                Console.WriteLine($"There is no SubToDo {subtodoName}");
                            }
                            else if (tempSubToDo.ToDoId != tempToDo.Id)
                            {
                                Console.WriteLine($"There is no SubToDo {subtodoName} in ToDo {todoName}");
                            }
                            else
                            {
                                subtodoService.ChangeSubToDoStatusByName(subtodoName, !tempSubToDo.Status, connectionString);
                                Console.WriteLine($"SubToDo {subtodoName} status successfully updated");
                            }
                        }
                    }
                }

                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        case 10:
            {
                Console.Write("Enter ToDo name which SubToDos you want to get: ");
                string? todoName = Console.ReadLine();

                if (string.IsNullOrEmpty(todoName))
                {
                    Console.WriteLine("ToDo name can't be null or empty");
                }
                else
                {
                    ToDo? tempToDo = todoService.GetToDoByName(todoName, connectionString);

                    if (tempToDo == null)
                    {
                        Console.WriteLine($"There is no ToDo {todoName}");
                    }
                    else
                    {
                        Console.WriteLine($"Id: {tempToDo.Id} - Name: {tempToDo.Name} - Progress: {tempToDo.Progress}");

                        subtodos = subtodoService.GetAllSubToDo(tempToDo.Id, connectionString);

                        if (subtodos.Count == 0)
                        {
                            Console.WriteLine("\t--- SubToDo list is empty ---");
                        }
                        else
                        {
                            foreach (var sub in subtodos)
                            {
                                Console.Write($"\tId: {sub.Id} - Name: {sub.Name} - Description: {sub.Description} - Deadline: {sub.Deadline} - ");
                                if (sub.Status)
                                {
                                    Console.WriteLine("Status: completed");
                                }
                                else
                                {
                                    Console.WriteLine("Status: in progress");
                                }
                            }
                        }
                    }
                }

                break;
            }
        case 11:
            {
                Console.WriteLine("Have a great day! Bye");
                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
        default:
            {
                Console.WriteLine("You chosen wrong option. Try again");
                Console.WriteLine("Press enter to continue");
                Console.Read();
                break;
            }
    }
}