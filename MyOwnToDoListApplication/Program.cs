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
    Console.WriteLine("6) Exit");

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
    }
}