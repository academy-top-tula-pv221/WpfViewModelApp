using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace WpfViewModelApp
{
    public interface IFileService
    {
        List<Employee>? Open(string fileName);
        void Save(string fileName, List<Employee> employees); 
    }

    public class JsonFileService : IFileService
    {
        public List<Employee>? Open(string fileName)
        {
            List<Employee>? employees = new();
            using(FileStream stream = new(fileName, FileMode.OpenOrCreate))
            {
                employees = JsonSerializer.Deserialize<List<Employee>>(stream);
            }
            return employees;
        }

        public void Save(string fileName, List<Employee> employees)
        {
            using (FileStream stream = new(fileName, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<Employee>>(stream, employees);
            }
        }
    }

}
