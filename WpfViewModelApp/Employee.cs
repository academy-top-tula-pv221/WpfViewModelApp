using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewModelApp
{
    public class Employee
    {
        public string? Name { set; get; }
        public DateTime BirthDate { set; get; } = DateTime.Now;
        public decimal Salary { set; get; }
    }
    
    public class EmployeeModel : INotifyPropertyChanged
    {
        //string? name;
        //DateTime birthDate;
        //decimal salary;

        Employee employee;

        public string? Name
        {
            get => employee.Name;
            set
            {
                if(employee.Name != value)
                {
                    employee.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public DateTime BirthDate
        {
            get => employee.BirthDate;
            set
            {
                if (employee.BirthDate != value)
                {
                    employee.BirthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }

        public decimal Salary
        {
            get => employee.Salary;
            set
            {
                if (employee.Salary != value)
                {
                    employee.Salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    
}
