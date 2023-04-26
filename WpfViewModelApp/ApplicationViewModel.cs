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
        Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; set; }

        public RoutedCommand CommandEmployeeAdd { get; set; } = new();

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
                                  Employees.Insert(0, employee);
                                  SelectedEmployee = employee;
                              }
                        )
                    );
            }
        }
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

        public ApplicationViewModel()
        {
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
