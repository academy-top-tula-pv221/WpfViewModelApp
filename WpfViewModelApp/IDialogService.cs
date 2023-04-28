using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfViewModelApp
{
    public interface IDialogService
    {
        string FileName { set; get; }
        void Message(string message);
        bool OpenFile();
        bool SaveFile();
    }

    public class EmployeeDialogService : IDialogService
    {
        public string FileName { get; set; } = "employees.json";

        public void Message(string message)
        {
            MessageBox.Show(message);
        }

        public bool OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = FileName;
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = FileName;
            if (saveFileDialog.ShowDialog() == true)
            {
                FileName = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
    }
}
