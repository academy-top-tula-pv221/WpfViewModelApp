using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfViewModelApp
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        IDialogService dialogService;
        IFileService fileService;


        Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                if (selectedEmployee != value)
                {
                    selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }
            }
        }
        public ObservableCollection<Employee> Employees { get; set; }

        EmployeeCommand commandAdd;
        public EmployeeCommand CommandAdd
        {
            get
            {
                return commandAdd ??
                    (
                    commandAdd = new EmployeeCommand
                        (
                              obj =>
                              {
                                  Employee employee = new();
                                  NewWindow newWindow = new(ref employee);
                                  if(newWindow.ShowDialog() == true)
                                  {
                                      Employees.Insert(0, employee);
                                      SelectedEmployee = employee;
                                  }
                                  
                              }
                        )
                    );
            }
        }

        EmployeeCommand commandRemove;
        public EmployeeCommand CommandRemove
        {
            get
            {
                return commandRemove ??
                    (commandRemove = new EmployeeCommand(
                        obj =>
                        {
                            if (obj is Employee employee)
                            {
                                Employees.Remove(employee);
                            }

                        },
                        obj => Employees.Count > 0
                        ));
            }
        }
        
        EmployeeCommand commandDublicate;
        public EmployeeCommand CommandDublicate
        {
            get
            {
                return commandDublicate ??
                    (commandDublicate = new EmployeeCommand(
                        obj =>
                        {
                            if (obj is Employee employee)
                            {
                                Employee employeeDublicte = new Employee()
                                {
                                    Name = employee.Name,
                                    BirthDate = employee.BirthDate,
                                    Salary = employee.Salary
                                };
                                Employees.Insert(0, employeeDublicte);
                            }
                        },
                        obj => Employees.Count > 0
                    )); ;
            }
        }

        EmployeeCommand commandSave;
        public EmployeeCommand CommandSave
        {
            get
            {
                return commandSave ??
                    (commandSave = new(
                        obj =>
                        {
                            try
                            {
                                if(dialogService.SaveFile() == true)
                                {
                                    fileService.Save(dialogService.FileName, Employees.ToList());
                                    dialogService.Message("Employees saved");
                                }
                            }
                            catch(Exception ex)
                            {
                                dialogService.Message(ex.ToString());
                            }
                        }
                    ));
            }
        }
        
        EmployeeCommand commandOpen;
        public EmployeeCommand CommandOpen
        {
            get
            {
                return commandOpen ??
                    (commandOpen = new(
                        obj =>
                        {
                            try
                            {
                                if(dialogService.OpenFile() == true)
                                {
                                    var employees = fileService.Open(dialogService.FileName);
                                    Employees.Clear();
                                    foreach (var e in employees)
                                        Employees.Add(e);
                                    dialogService.Message("Employees readed");
                                }
                            }
                            catch( Exception ex )
                            {
                                dialogService.Message(ex.ToString());
                            }
                        }
                    ));
            }
        }
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            Employees = new ObservableCollection<Employee>()
            {
                new(){ Name = "Bobby", BirthDate = new DateTime(1999, 4, 11), Salary = 90000},
                new(){ Name = "Philipp", BirthDate = new DateTime(2000, 11, 21), Salary = 75000},
                new(){ Name = "Hanry", BirthDate = new DateTime(1985, 2, 5), Salary = 110000},
                new(){ Name = "Whiliam", BirthDate = new DateTime(1993, 9, 22), Salary = 87000},
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
